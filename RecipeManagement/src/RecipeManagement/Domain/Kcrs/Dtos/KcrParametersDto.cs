namespace RecipeManagement.Domain.Kcrs.Dtos;

using RecipeManagement.Resources;

/// <summary>
/// Paramètres de la requête GET /api/v1/kcrs (query string).
///
/// - PageNumber / PageSize : hérités de BasePaginationParameters (pagination).
/// - Filters   : filtre QueryKit, ex. ?Filters=IsActive == true &amp;&amp; KcrName @=* "chef"
/// - SortOrder : tri QueryKit,   ex. ?SortOrder=KcrName  ou  ?SortOrder=-CreatedOn (décroissant)
/// </summary>
public sealed class KcrParametersDto : BasePaginationParameters
{
    /// <summary>Expression de filtre QueryKit (optionnelle).</summary>
    public string? Filters { get; set; }

    /// <summary>Expression de tri QueryKit (optionnelle).</summary>
    public string? SortOrder { get; set; }
}
