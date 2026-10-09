namespace RecipeManagement.Domain.Kcrs.Mappings;

using RecipeManagement.Domain.Qualifications.Mappings;
using RecipeManagement.Domain.Kcrs.Dtos;
using RecipeManagement.Domain.Kcrs.Models;
using Riok.Mapperly.Abstractions;

/// <summary>
/// Conversions entre DTOs (couche API) et modèles / entité (couche domaine).
/// Mapperly génère le code de ces méthodes à la compilation :
/// on écrit seulement les signatures "partial".
/// </summary>
[Mapper]

[UseStaticMapper(typeof(QualificationMapper))]
public static partial class KcrMapper
{
    /// <summary>DTO reçu en POST -> modèle accepté par Kcr.Create().</summary>
    public static partial KcrForCreation ToKcrForCreation(this KcrForCreationDto kcrForCreationDto);

    /// <summary>DTO reçu en PUT -> modèle accepté par kcr.Update().</summary>
    public static partial KcrForUpdate ToKcrForUpdate(this KcrForUpdateDto kcrForUpdateDto);

    /// <summary>Entité -> DTO renvoyé au client.</summary>
  
    public static partial KcrDto ToKcrDto(this Kcr kcr);

    /// <summary>
    /// Projection IQueryable : EF Core traduit le mapping en SQL
    /// (SELECT uniquement des colonnes utiles) pour la liste paginée.
    /// </summary>
    public static partial IQueryable<KcrDto> ToKcrDtoQueryable(this IQueryable<Kcr> queryable);
}
