using RecipeManagement.Domain.Qualifications.Dtos;

namespace RecipeManagement.Domain.Kcrs.Dtos;

/// <summary>
/// Ce que l'API renvoie au client pour un rôle (lecture).
/// L'entité Kcr ne sort jamais du domaine : on renvoie toujours ce DTO.
/// </summary>
public sealed record KcrDto
{
    /// <summary>Identifiant du rôle (kcr_role_id).</summary>
    public Guid Id { get; set; }

    /// <summary>Nom du rôle (role_name).</summary>
    public string KcrName { get; set; } = default!;

    /// <summary>Description du rôle (description).</summary>
    public string? Description { get; set; }

    /// <summary>Rôle actif ou non (is_active).</summary>
    public bool IsActive { get; set; }

    /// <summary>Date de création (created_at), remplie par l'audit de BaseEntity.</summary>
    public DateTimeOffset CreatedOn { get; set; }

    /// <summary>Fiche de qualification du rôle, ou null s'il n'en a pas.</summary>
    public QualificationDto? Qualification { get; set; }
}

