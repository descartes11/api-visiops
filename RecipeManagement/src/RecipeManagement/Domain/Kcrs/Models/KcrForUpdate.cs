namespace RecipeManagement.Domain.Kcrs.Models;

/// <summary>
/// Modèle interne au domaine contenant les données nécessaires pour modifier un rôle.
/// Il est passé à <see cref="Kcr.Update(KcrForUpdate)"/>.
///
/// Toutes les propriétés modifiables sont présentes : la mise à jour
/// remplace l'état complet du rôle (comportement d'un PUT).
/// </summary>
public sealed record KcrForUpdate
{
    /// <summary>
    /// Nouveau nom du rôle (colonne role_name). Obligatoire et unique.
    /// </summary>
    public string KcrName { get; set; }

    /// <summary>
    /// Nouvelle description (colonne description). Optionnelle.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Nouvel état actif/inactif (colonne is_active).
    /// </summary>
    public bool IsActive { get; set; }

    // Remarque : pas d'Id ici. L'Id est passé séparément (dans l'URL du PUT)
    // et sert à retrouver le rôle avant d'appeler kcr.Update(...).
    // LastModifiedOn / LastModifiedBy sont remplis automatiquement par l'audit.
}

