using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.PropertyType;
using RealEstateApp.Core.Application.Features.PropertyType.Commands.CreatePropertyType;
using RealEstateApp.Core.Application.Features.PropertyType.Commands.DeletePropertyType;
using RealEstateApp.Core.Application.Features.PropertyType.Commands.UpdatePropertyType;
using RealEstateApp.Core.Application.Features.PropertyType.Queries.GetAllPropertyType;
using RealEstateApp.Core.Application.Features.PropertyType.Queries.GetByIdPropertyType;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealEstateApp.WebApi.Controllers.v1
{

    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de  tipo de propiedades")]


    [ApiController]
    public class PropertyTypeController : BaseApiController
    {

        [Authorize(Roles = "Administrador")]
        [HttpPost("Create")]
        [SwaggerOperation( Summary = "Creacion de tipo de propiedad",Description = "Recibe los parametros necesarios para crear una nuevo tipo de propiedad")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreatePropertyTypeCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("Update/{id}")]
        [SwaggerOperation(Summary = "Actualizacion de tipo de propiedad", Description = "Recibe los parametros necesarios para modificar un tipo de propiedad existente")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyTypeUpdateResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdatePropertyTypeCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("Delete/{id}")]
        [SwaggerOperation(Summary = "Eliminar un tipo de propiedad",Description = "Recibe los parametros necesarios para eliminar un tipo de propiedad existente, al eliminar un tipo de propiedad " +"se elimina las propiedades asociada con esta")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeletePropertyTypeByIdCommand { Id = id });
            return NoContent();
        }

        [Authorize(Roles = "Administrador,Desarrollador")]

        [HttpGet("List")]
        [SwaggerOperation(Summary = "Listado de tipos de propiedades", Description = "Obtiene todas los tipos de  propiedades del sistema ")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyTypeDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Get()
        {
            var propertiesType = await Mediator.Send(new GetAllPropertiesTypeQuery());
            if (propertiesType == null || propertiesType.Count <= 0)
            {
                return NoContent();
            }

            return Ok(propertiesType);
        }

        [Authorize(Roles = "Administrador,Desarrollador")]
        [HttpGet("GetById/{id}")]
        [SwaggerOperation(Summary = "Tipos de propiedades por Id", Description = "Obtiene un tipo de propiedad utilizando el id de esta ")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyTypeDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Get(int id)
        {
            var propertyType = await Mediator.Send(new GetByIdPropertytTypeQuery() { Id = id });
            if (propertyType == null)
            {
                return NoContent();
            }

            return Ok(propertyType);
        }

    }
}
