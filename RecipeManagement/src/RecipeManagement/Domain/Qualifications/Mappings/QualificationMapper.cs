namespace RecipeManagement.Domain.Qualifications.Mappings;

using RecipeManagement.Domain.Qualifications.Dtos;
using RecipeManagement.Domain.Qualifications.Models;
using Riok.Mapperly.Abstractions;

/// <summary>Conversions DTO &lt;-&gt; domaine, générées par Mapperly à la compilation.</summary>
[Mapper]
public static partial class QualificationMapper
{
    public static partial QualificationForCreation ToQualificationForCreation(this QualificationForCreationDto dto);
    public static partial QualificationForUpdate ToQualificationForUpdate(this QualificationForUpdateDto dto);

    /// <summary>La navigation Kcr n'est pas copiée dans le DTO (seul KcrId l'est).</summary>
    [MapperIgnoreSource(nameof(Qualification.Kcr))]
    public static partial QualificationDto ToQualificationDto(this Qualification qualification);
}
