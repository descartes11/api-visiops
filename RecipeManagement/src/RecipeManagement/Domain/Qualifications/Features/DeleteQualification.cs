namespace RecipeManagement.Domain.Qualifications.Features;

using RecipeManagement.Databases;
using RecipeManagement.Domain.Qualifications.Dtos;
using RecipeManagement.Domain.Qualifications.Mappings;
using RecipeManagement.Domain.Kcrs;
using RecipeManagement.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Supprime la fiche de qualification d'un rôle (DELETE /api/v1/kcrs/{kcrId}/qualification).
/// Suppression logique (IsDeleted = true), comme pour Recipe et Kcr.
/// </summary>
public static class DeleteQualification
{
    public sealed record Command(Guid KcrId) : IRequest;

    public sealed class Handler(RecipesDbContext dbContext) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var qualification = await dbContext.Qualifications
                .FirstOrDefaultAsync(q => q.KcrId == request.KcrId, cancellationToken);

            if (qualification is null)
                throw new NotFoundException(nameof(Qualification), request.KcrId);

            dbContext.Qualifications.Remove(qualification);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
