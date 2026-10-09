
namespace RecipeManagement.Domain.Kcrs.Features;

using RecipeManagement.Databases;
using RecipeManagement.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Feature : supprimer un rôle (DELETE /api/v1/kcrs/{id}).
///
/// Suppression LOGIQUE : comme pour Recipe, le DbContext intercepte le Remove()
/// et passe IsDeleted à true au lieu d'effacer la ligne (voir BaseEntity.UpdateIsDeleted).
/// Le rôle disparaît des lectures mais reste en base.
///
/// Variante : pour seulement le rendre inactif (is_active = false), remplacer
/// dbContext.Kcrs.Remove(kcr) par kcr.Deactivate().
/// </summary>
public static class DeleteKcr
{
    public sealed record Command(Guid KcrId) : IRequest;

    public sealed class Handler(RecipesDbContext dbContext) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var kcr = await dbContext.Kcrs
                .FirstOrDefaultAsync(r => r.Id == request.KcrId, cancellationToken);

            if (kcr is null)
                throw new NotFoundException(nameof(Kcr), request.KcrId);

            dbContext.Kcrs.Remove(kcr);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
