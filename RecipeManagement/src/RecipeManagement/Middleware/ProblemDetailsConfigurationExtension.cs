// source: https://github.com/jasontaylordev/CleanArchitecture/blob/main/src/WebUI/Filters/ApiExceptionFilterAttribute.cs

namespace RecipeManagement.Middleware;

using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using RecipeManagement.Exceptions;
using ProblemDetailsOptions = Hellang.Middleware.ProblemDetails.ProblemDetailsOptions;

public static class ProblemDetailsConfigurationExtension
{ 
    public static void ConfigureProblemDetails(ProblemDetailsOptions options)
    {
        options.MapFluentValidationException();
        options.MapValidationException();
        options.MapToStatusCode<ForbiddenAccessException>(StatusCodes.Status401Unauthorized);
        options.MapToStatusCode<NoRolesAssignedException>(StatusCodes.Status403Forbidden);
        options.MapToStatusCode<NotFoundException>(StatusCodes.Status404NotFound);
        options.MapToStatusCode<ArgumentException>(StatusCodes.Status400BadRequest);
        options.MapToStatusCode<InvalidOperationException>(StatusCodes.Status409Conflict);
        options.MapUniqueViolationException();

        // You can configure the middleware to re-throw certain types of exceptions, all exceptions or based on a predicate.
        // This is useful if you have upstream middleware that needs to do additional handling of exceptions.
        // options.Rethrow<NotSupportedException>();

        options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);
        options.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);

        // You can configure the middleware to re-throw certain types of exceptions, all exceptions or based on a predicate.
        // This is useful if you have upstream middleware that  needs to do additional handling of exceptions.
        // options.Rethrow<NotSupportedException>();

        // You can configure the middleware to ignore any exceptions of the specified type.
        // This is useful if you have upstream middleware that  needs to do additional handling of exceptions.
        // Note that unlike Rethrow, additional information will not be added to the exception.
        // options.Ignore<DivideByZeroException>();

        // Because exceptions are handled polymorphically, this will act as a "catch all" mapping, which is why it's added last.
        // If an exception other than NotImplementedException and HttpRequestException is thrown, this will handle it.
        options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
    }
    
    // Violation d'un index unique PostgreSQL (SqlState 23505) -> 409 au lieu de 500.
    // Les autres DbUpdateException restent traitées par le mappage 500 par défaut.
    private static void MapUniqueViolationException(this ProblemDetailsOptions options) =>
        options.Map<DbUpdateException>(
            (ctx, ex) => ex.InnerException is PostgresException { SqlState: PostgresErrorCodes.UniqueViolation },
            (ctx, ex) => new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Conflict",
                Detail = "Une valeur unique existe déjà (par exemple, un rôle portant ce nom)."
            });

    private static void MapFluentValidationException(this ProblemDetailsOptions options) =>
        options.Map<FluentValidation.ValidationException>((ctx, ex) =>
        {
            var factory = ctx.RequestServices.GetRequiredService<ProblemDetailsFactory>();

            var errors = ex.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    x => x.Key,
                    x => x.Select(x => x.ErrorMessage).ToArray());

            return factory.CreateValidationProblemDetails(ctx, errors);
        });
    
    private static void MapValidationException(this ProblemDetailsOptions options) =>
        options.Map<ValidationException>((ctx, ex) =>
        {
            var factory = ctx.RequestServices.GetRequiredService<ProblemDetailsFactory>();

            var errors = ex.Errors
                .GroupBy(x => x.Key)
                .ToDictionary(
                    x => x.Key,
                    x => x.Select(x => x.Value.ToString()).ToArray());

            return factory.CreateValidationProblemDetails(ctx, errors);
        });
}
