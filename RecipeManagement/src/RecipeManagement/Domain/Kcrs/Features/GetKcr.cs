namespace RecipeManagement.Domain.Kcrs.Features;

using RecipeManagement.Databases;
using RecipeManagement.Domain.Kcrs.Dtos;
using RecipeManagement.Domain.Kcrs.Mappings;
using RecipeManagement.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Feature : lire un rôle par son Id (GET /api/v1/kcrs/{id}).
/// </summary>
public static class GetKcr
{
    public sealed record Query(Guid KcrId) : IRequest<KcrDto>;

    public sealed class Handler(RecipesDbContext dbContext) : IRequestHandler<Query, KcrDto>
    {
        public async Task<KcrDto> Handle(Query request, CancellationToken cancellationToken)
        {
            // AsNoTracking : lecture seule, plus rapide (pas de suivi des changements).
            var kcr = await dbContext.Kcrs
                .AsNoTracking()
                .Include(r => r.Qualification)
                .FirstOrDefaultAsync(r => r.Id == request.KcrId, cancellationToken);

            // Rôle introuvable -> le middleware d'exceptions renvoie un 404.
            if (kcr is null)
                throw new NotFoundException(nameof(Kcr), request.KcrId);

            return kcr.ToKcrDto();
        }
    }
}
