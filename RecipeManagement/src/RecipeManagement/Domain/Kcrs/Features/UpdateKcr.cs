namespace RecipeManagement.Domain.Kcrs.Features;

using RecipeManagement.Databases;
using RecipeManagement.Domain.Kcrs.Dtos;
using RecipeManagement.Domain.Kcrs.Mappings;
using RecipeManagement.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Feature : modifier un rôle (PUT /api/v1/kcrs/{id}).
/// </summary>
public static class UpdateKcr
{
    public sealed record Command(Guid KcrId, KcrForUpdateDto UpdatedKcrData) : IRequest;

    public sealed class Handler(RecipesDbContext dbContext) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            // Pas de AsNoTracking ici : EF doit suivre l'entité pour détecter les changements.
            var kcrToUpdate = await dbContext.Kcrs
                .FirstOrDefaultAsync(r => r.Id == request.KcrId, cancellationToken);

            if (kcrToUpdate is null)
                throw new NotFoundException(nameof(Kcr), request.KcrId);

            // La règle métier (nom obligatoire) est appliquée dans Kcr.Update().
            var kcrToApply = request.UpdatedKcrData.ToKcrForUpdate();
            kcrToUpdate.Update(kcrToApply);

            // SaveChanges remplit LastModifiedOn / LastModifiedBy (audit).
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
