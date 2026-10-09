namespace RecipeManagement.Controllers.v1;

using System.Text.Json;
using Asp.Versioning;
using RecipeManagement.Domain.Kcrs.Dtos;
using RecipeManagement.Domain.Kcrs.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Endpoints REST des rôles. Le contrôleur ne contient aucune logique :
/// il transforme la requête HTTP en commande / requête MediatR.
/// </summary>
[ApiController]
[Route("api/v{v:apiVersion}/kcrs")]
[ApiVersion("1.0")]
public sealed class KcrsController(IMediator mediator) : ControllerBase
{
    /// <summary>GET /api/v1/kcrs — liste paginée, filtrable, triable.</summary>
    [HttpGet(Name = "GetKcrs")]
    public async Task<IActionResult> GetKcrs([FromQuery] KcrParametersDto kcrParametersDto)
    {
        var queryResponse = await mediator.Send(new GetKcrList.Query(kcrParametersDto));

        // Infos de pagination renvoyées dans l'en-tête X-Pagination (comme pour Recipes).
        var paginationMetadata = new
        {
            totalCount = queryResponse.TotalCount,
            pageSize = queryResponse.PageSize,
            currentPageSize = queryResponse.CurrentPageSize,
            currentStartIndex = queryResponse.CurrentStartIndex,
            currentEndIndex = queryResponse.CurrentEndIndex,
            pageNumber = queryResponse.PageNumber,
            totalPages = queryResponse.TotalPages,
            hasPrevious = queryResponse.HasPrevious,
            hasNext = queryResponse.HasNext
        };
        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

        return Ok(queryResponse);
    }

    /// <summary>GET /api/v1/kcrs/{id} — un rôle. 404 s'il n'existe pas.</summary>
    [HttpGet("{kcrId:guid}", Name = "GetKcr")]
    public async Task<ActionResult<KcrDto>> GetKcr(Guid kcrId)
    {
        var kcr = await mediator.Send(new GetKcr.Query(kcrId));
        return Ok(kcr);
    }

    /// <summary>POST /api/v1/kcrs — crée un rôle. Renvoie 201 + en-tête Location.</summary>
    [HttpPost(Name = "AddKcr")]
    public async Task<ActionResult<KcrDto>> AddKcr([FromBody] KcrForCreationDto kcrForCreation)
    {
        var kcrReturn = await mediator.Send(new AddKcr.Command(kcrForCreation));
        return CreatedAtRoute("GetKcr", new { kcrId = kcrReturn.Id }, kcrReturn);
    }

    /// <summary>PUT /api/v1/kcrs/{id} — modifie un rôle. Renvoie 204.</summary>
    [HttpPut("{kcrId:guid}", Name = "UpdateKcr")]
    public async Task<IActionResult> UpdateKcr(Guid kcrId, [FromBody] KcrForUpdateDto kcr)
    {
        await mediator.Send(new UpdateKcr.Command(kcrId, kcr));
        return NoContent();
    }

    /// <summary>DELETE /api/v1/kcrs/{id} — suppression logique. Renvoie 204.</summary>
    [HttpDelete("{kcrId:guid}", Name = "DeleteKcr")]
    public async Task<ActionResult> DeleteKcr(Guid kcrId)
    {
        await mediator.Send(new DeleteKcr.Command(kcrId));
        return NoContent();
    }
}
