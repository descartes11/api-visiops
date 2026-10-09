namespace RecipeManagement.Domain.Qualifications.Features;

using RecipeManagement.Databases;
using RecipeManagement.Domain.Qualifications.Dtos;
using RecipeManagement.Domain.Qualifications.Mappings;
using RecipeManagement.Domain.Kcrs;
using RecipeManagement.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

/// <summary>Modifie la fiche de qualification d'un rôle (PUT /api/v1/kcrs/{kcrId}/qualification).</summary>
public static class UpdateQualification
{
    public sealed record Command(Guid KcrId, QualificationForUpdateDto UpdatedData) : IRequest;

    public sealed class Handler(RecipesDbContext dbContext) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            // Entité suivie par EF (pas de AsNoTracking) pour détecter les changements
            var qualification = await dbContext.Qualifications
                .FirstOrDefaultAsync(q => q.KcrId == request.KcrId, cancellationToken);

            if (qualification is null)
                throw new NotFoundException(nameof(Qualification), request.KcrId);

            qualification.Update(request.UpdatedData.ToQualificationForUpdate());
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
