namespace RecipeManagement.Controllers.v1;

using Asp.Versioning;
using RecipeManagement.Domain.Qualifications.Dtos;
using RecipeManagement.Domain.Qualifications.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// La fiche de qualification est une sous-ressource du rôle :
/// /api/v1/kcrs/{kcrId}/qualification (au singulier : une seule fiche par rôle).
/// </summary>
[ApiController]
[Route("api/v{v:apiVersion}/kcrs/{kcrId:guid}/qualification")]
[ApiVersion("1.0")]
public sealed class KcrQualificationController(IMediator mediator) : ControllerBase
{
    /// <summary>GET — lit la fiche du rôle. 404 si le rôle n'en a pas.</summary>
    [HttpGet(Name = "GetKcrQualification")]
    public async Task<ActionResult<QualificationDto>> Get(Guid kcrId)
        => Ok(await mediator.Send(new GetQualification.Query(kcrId)));

    /// <summary>POST — crée la fiche du rôle. 201 + Location.</summary>
    [HttpPost(Name = "AddKcrQualification")]
    public async Task<ActionResult<QualificationDto>> Add(Guid kcrId, [FromBody] QualificationForCreationDto dto)
    {
        var created = await mediator.Send(new AddQualification.Command(kcrId, dto));
        return CreatedAtRoute("GetKcrQualification", new { kcrId }, created);
    }

    /// <summary>PUT — remplace toute la fiche. 204.</summary>
    [HttpPut(Name = "UpdateKcrQualification")]
    public async Task<IActionResult> Update(Guid kcrId, [FromBody] QualificationForUpdateDto dto)
    {
        await mediator.Send(new UpdateQualification.Command(kcrId, dto));
        return NoContent();
    }

    /// <summary>DELETE — supprime (logiquement) la fiche. 204.</summary>
    [HttpDelete(Name = "DeleteKcrQualification")]
    public async Task<IActionResult> Delete(Guid kcrId)
    {
        await mediator.Send(new DeleteQualification.Command(kcrId));
        return NoContent();
    }
}
