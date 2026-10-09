namespace RecipeManagement.Domain.Kcrs.Models;

/// <summary>
/// Modèle interne au domaine contenant les données nécessaires pour créer un rôle.
/// Il est passé à <see cref="Kcr.Create(KcrForCreation)"/>.
/// 
/// Différence avec KcrForCreationDto :
/// - le DTO est ce que l'API reçoit (couche présentation) ;
/// - ce modèle est ce que l'entité accepte (couche domaine).
/// Le mapper (KcrMapper) convertit l'un vers l'autre.
/// </summary>
public sealed record KcrForCreation
{
    /// <summary>
    /// Nom du rôle (colonne role_name). Obligatoire et unique.
    /// La validation (non vide) est faite dans l'entité Kcr.
    /// </summary>
    public string KcrName { get; set; }

    /// <summary>
    /// Description libre du rôle (colonne description). Optionnelle.
    /// </summary>
    public string? Description { get; set; }


    /// <summary>
    /// Indique si le rôle est actif à sa création (colonne is_active).
    /// Par défaut : true.
    /// </summary>
    public bool IsActive { get; set; } = true;

    // Remarque : pas d'Id ni de CreatedOn ici.
    // - Id est généré automatiquement par BaseEntity (Guid.NewGuid()).
    // - CreatedOn / CreatedBy sont remplis par l'audit (UpdateCreationProperties)
    //   au moment de la sauvegarde dans le DbContext.
}

