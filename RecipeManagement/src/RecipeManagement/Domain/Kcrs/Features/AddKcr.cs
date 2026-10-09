namespace RecipeManagement.Domain.Kcrs.Features;

using RecipeManagement.Databases;
using RecipeManagement.Domain.Kcrs.Dtos;
using RecipeManagement.Domain.Kcrs.Mappings;
using MediatR;

/// <summary>
/// Feature : créer un rôle (POST /api/v1/kcrs).
/// Chemin : Controller -> MediatR -> Handler -> DbContext.
/// </summary>
public static class AddKcr
{
    /// <summary>La commande envoyée par le contrôleur via mediator.Send(...).</summary>
    public sealed record Command(KcrForCreationDto KcrToAdd) : IRequest<KcrDto>;

    public sealed class Handler(RecipesDbContext dbContext) : IRequestHandler<Command, KcrDto>
    {
        public async Task<KcrDto> Handle(Command request, CancellationToken cancellationToken)
        {
            // 1. DTO -> modèle de domaine
            var kcrToAdd = request.KcrToAdd.ToKcrForCreation();

            // 2. Création via la fabrique (validation + événement KcrCreated)
            var kcr = Kcr.Create(kcrToAdd);

            // 3. Persistance : SaveChanges remplit aussi CreatedOn / CreatedBy (audit)
            //    et publie les événements de domaine.
            await dbContext.Kcrs.AddAsync(kcr, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            // 4. Entité -> DTO renvoyé au client
            return kcr.ToKcrDto();
        }
    }
}
