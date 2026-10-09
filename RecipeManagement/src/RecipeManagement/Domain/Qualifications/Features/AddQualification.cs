namespace RecipeManagement.Domain.Qualifications.Features;

using RecipeManagement.Databases;
using RecipeManagement.Domain.Qualifications.Dtos;
using RecipeManagement.Domain.Qualifications.Mappings;
using RecipeManagement.Domain.Kcrs;
using RecipeManagement.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Crée la fiche de qualification d'un rôle (POST /api/v1/kcrs/{kcrId}/qualification).
/// Refuse si le rôle n'existe pas (404) ou s'il a déjà une fiche (409 / ValidationException).
/// </summary>
public static class AddQualification
{
    public sealed record Command(Guid KcrId, QualificationForCreationDto QualificationToAdd) : IRequest<QualificationDto>;

    public sealed class Handler(RecipesDbContext dbContext) : IRequestHandler<Command, QualificationDto>
    {
        public async Task<QualificationDto> Handle(Command request, CancellationToken cancellationToken)
        {
            // 1. Le rôle doit exister
            var kcrExists = await dbContext.Kcrs.AnyAsync(r => r.Id == request.KcrId, cancellationToken);
            if (!kcrExists)
                throw new NotFoundException(nameof(Kcr), request.KcrId);

            // 2. Relation 1-1 : une seule fiche par rôle
            var alreadyHasOne = await dbContext.Qualifications.AnyAsync(q => q.KcrId == request.KcrId, cancellationToken);
            if (alreadyHasOne)
                throw new InvalidOperationException("Ce rôle possède déjà une fiche de qualification. Utilisez PUT pour la modifier.");

            // 3. Création + sauvegarde
            var qualification = Qualification.Create(request.KcrId, request.QualificationToAdd.ToQualificationForCreation());
            await dbContext.Qualifications.AddAsync(qualification, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return qualification.ToQualificationDto();
        }
    }
}
