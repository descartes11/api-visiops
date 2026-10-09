namespace RecipeManagement.Domain.Kcrs.Features;

using RecipeManagement.Databases;
using RecipeManagement.Domain.Kcrs.Dtos;
using RecipeManagement.Domain.Kcrs.Mappings;
using RecipeManagement.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QueryKit;
using QueryKit.Configuration;

/// <summary>
/// Feature : liste paginée, filtrable et triable (GET /api/v1/kcrs).
/// Même mécanique que GetRecipeList (QueryKit + PagedList).
/// </summary>
public static class GetKcrList
{
    public sealed record Query(KcrParametersDto QueryParameters) : IRequest<PagedList<KcrDto>>;

    public sealed class Handler(RecipesDbContext dbContext) : IRequestHandler<Query, PagedList<KcrDto>>
    {
        public async Task<PagedList<KcrDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            // 1. Point de départ : tous les rôles (les supprimés sont exclus
            //    automatiquement par le filtre global "soft delete" du DbContext).
            var collection = dbContext.Kcrs.AsNoTracking();

            // 2. Filtres et tri passés dans la query string.
            var queryKitData = new QueryKitData
            {
                Filters = request.QueryParameters.Filters,
                SortOrder = request.QueryParameters.SortOrder ?? "KcrName",
                Configuration = new CustomQueryKitConfiguration()
            };
            var appliedCollection = collection.ApplyQueryKit(queryKitData);

            // 3. Projection en DTO (traduite en SQL) puis pagination.
            var dtoCollection = appliedCollection.ToKcrDtoQueryable();

            return await PagedList<KcrDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}
