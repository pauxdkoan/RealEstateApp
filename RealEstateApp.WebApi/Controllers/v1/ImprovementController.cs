using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Improvement;
using RealEstateApp.Core.Application.Dtos.SaleType;
using RealEstateApp.Core.Application.Features.Improvement.Commands.CreateImprovement;
using RealEstateApp.Core.Application.Features.Improvement.Commands.DeleteImprovement;
using RealEstateApp.Core.Application.Features.Improvement.Commands.UpdateImprovement;
using RealEstateApp.Core.Application.Features.Improvement.Queries.GetAllImprovement;
using RealEstateApp.Core.Application.Features.Improvement.Queries.GetByIdImprovement;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealEstateApp.WebApi.Controllers.v1
{

    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de mejoras")]


    [ApiController]
    public class ImprovementController : BaseApiController
    {

        [Authorize(Roles = "Administrador")]
        [HttpPost("Create")]
        [SwaggerOperation( Summary = "Creacion de mejora",Description = "Recibe los parametros necesarios para crear una nueva mejora")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreateImprovementCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("Update/{id}")]
        [SwaggerOperation(Summary = "Actualizacion de tipo mejora", Description = "Recibe los parametros necesarios para modificar una mejora existente")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImprovementUpdateResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateImprovementCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("Delete/{id}")]
        [SwaggerOperation(Summary = "Eliminar una mejora",Description = "Recibe los parametros necesarios para eliminar una mejora existente, al eliminar una mejora " +"se elimina las propiedades asociada con esta")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteImprovementByIdCommand { Id = id });
            return NoContent();
        }
        [Authorize(Roles = "Administrador,Desarrollador")]
        [HttpGet("List")]
        [SwaggerOperation(Summary = "Listado de mejoras", Description = "Obtiene todas las mejora del sistema ")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SalesTypeDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Get()
        {
            var improvements = await Mediator.Send(new GetAllImprovementQuery());
            if (improvements == null || improvements.Count <= 0)
            {
                return NoContent();
            }

            return Ok(improvements);
        }

        [Authorize(Roles = "Administrador,Desarrollador")]
        [HttpGet("GetById/{id}")]
        [SwaggerOperation(Summary = "Mejora por id", Description = "Obtiene una mejora  utilizando el id de esta ")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImprovementDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Get(int id)
        {
            var improvement = await Mediator.Send(new GetByIdImprovementQuery() { Id = id });
            if (improvement == null)
            {
                return NoContent();
            }

            return Ok(improvement);
        }

    }
}
