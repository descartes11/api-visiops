namespace RecipeManagement.Domain.Qualifications.Features;

using RecipeManagement.Databases;
using RecipeManagement.Domain.Qualifications.Dtos;
using RecipeManagement.Domain.Qualifications.Mappings;
using RecipeManagement.Domain.Kcrs;
using RecipeManagement.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

/// <summary>Lit la fiche de qualification d'un rôle (GET /api/v1/kcrs/{kcrId}/qualification).</summary>
public static class GetQualification
{
    public sealed record Query(Guid KcrId) : IRequest<QualificationDto>;

    public sealed class Handler(RecipesDbContext dbContext) : IRequestHandler<Query, QualificationDto>
    {
        public async Task<QualificationDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var qualification = await dbContext.Qualifications
                .AsNoTracking()
                .FirstOrDefaultAsync(q => q.KcrId == request.KcrId, cancellationToken);

            if (qualification is null)
                throw new NotFoundException(nameof(Qualification), request.KcrId);

            return qualification.ToQualificationDto();
        }
    }
}
