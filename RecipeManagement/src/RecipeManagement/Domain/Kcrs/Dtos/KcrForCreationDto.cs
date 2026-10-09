namespace RecipeManagement.Domain.Kcrs.Dtos;

/// <summary>
/// Corps de la requête POST /api/v1/kcrs.
/// Pas d'Id ni de date : ils sont générés côté serveur.
/// </summary>
public sealed record KcrForCreationDto
{
    /// <summary>Nom du rôle. Obligatoire et unique.</summary>
    public string KcrName { get; set; } = default!;

    /// <summary>Description du rôle. Optionnelle.</summary>
    public string? Description { get; set; }

    /// <summary>Actif à la création. Vrai par défaut si absent du JSON.</summary>
    public bool IsActive { get; set; } = true;
}
