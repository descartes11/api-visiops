namespace RecipeManagement.Domain.Kcrs.Dtos;

/// <summary>
/// Corps de la requête PUT /api/v1/kcrs/{id}.
/// L'Id vient de l'URL, pas du corps. Toutes les valeurs sont remplacées (PUT).
/// </summary>
public sealed record KcrForUpdateDto
{
    /// <summary>Nouveau nom du rôle. Obligatoire et unique.</summary>
    public string KcrName { get; set; } = default!;

    /// <summary>Nouvelle description. Optionnelle.</summary>
    public string? Description { get; set; }

    /// <summary>Nouvel état actif / inactif.</summary>
    public bool IsActive { get; set; }
}

