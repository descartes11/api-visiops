namespace RecipeManagement.Domain.Kcrs;

using RecipeManagement.Domain.Qualifications;
using RecipeManagement.Domain.Kcrs.DomainEvents;
using RecipeManagement.Domain.Kcrs.Models;

/// <summary>
/// Entité correspondant à la table KCR_ROLE du modèle conceptuel.
///
/// Elle hérite de BaseEntity, qui fournit déjà :
/// - Id (kcr_role_id)            -> clé primaire Guid
/// - CreatedOn / CreatedBy       -> created_at (rempli par l'audit)
/// - LastModifiedOn / By         -> suivi des modifications
/// - IsDeleted                   -> suppression logique (soft delete)
/// - DomainEvents                -> événements à publier
/// </summary>
public class Kcr : BaseEntity
{
    // Les setters sont privés : on ne modifie un rôle qu'à travers
    // Create() et Update(), pour garder les règles métier au même endroit.

    /// <summary>Nom du rôle (role_name). Obligatoire.</summary>
    public string KcrName { get; private set; } = default!;

    /// <summary>Description du rôle (description). Optionnelle.</summary>
    public string? Description { get; private set; }

    /// <summary>Rôle actif ou non (is_active). Vrai par défaut.</summary>
    public bool IsActive { get; private set; } = true;

    public Qualification? Qualification { get; private set; }

    /// <summary>
    /// Fabrique : crée un nouveau rôle valide à partir des données de création.
    /// </summary>
    
    public static Kcr Create(KcrForCreation kcrForCreation)
    {
        var newKcr = new Kcr();

        newKcr.SetKcrName(kcrForCreation.KcrName); // validation incluse
        newKcr.Description = kcrForCreation.Description;
        newKcr.IsActive = kcrForCreation.IsActive;

        // On signale la création ; l'événement sera publié après la sauvegarde.
        newKcr.QueueDomainEvent(new KcrCreated { Kcr = newKcr });

        return newKcr;
    }

    /// <summary>
    /// Met à jour toutes les propriétés modifiables du rôle.
    /// </summary>
    public Kcr Update(KcrForUpdate kcrForUpdate)
    {
        SetKcrName(kcrForUpdate.KcrName);
        Description = kcrForUpdate.Description;
        IsActive = kcrForUpdate.IsActive;

        QueueDomainEvent(new KcrUpdated { Id = Id });

        return this;
    }

    /// <summary>
    /// Désactive le rôle sans le supprimer (il reste en base
    /// et les éléments qui y font référence restent valides).
    /// </summary
    public Kcr Deactivate()
    {
        IsActive = false;
        QueueDomainEvent(new KcrUpdated { Id = Id });
        return this;
    }

    /// <summary>
    /// Règle métier : le nom est obligatoire. On supprime aussi les espaces superflus.
    /// </summary>
    private void SetKcrName(string kcrName)
    {
        if (string.IsNullOrWhiteSpace(kcrName))
            throw new ArgumentException("Le nom du rôle est obligatoire.", nameof(kcrName));

        KcrName = kcrName.Trim();
    }

    /// <summary>
    /// Constructeur protégé requis par EF Core pour matérialiser l'entité.
    /// Utilisez Kcr.Create(...) dans le code applicatif.
    /// </summary>
    protected Kcr() { }
}
