using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Property;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Features.Properties.Queries.GetAllProperties;
using RealEstateApp.Core.Application.Features.Properties.Queries.GetByCodeProperty;
using RealEstateApp.Core.Application.Features.Properties.Queries.GetByIdProperty;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealEstateApp.WebApi.Controllers.v1
{
   
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de propiedades")]

    [Authorize(Roles = "Administrador,Desarrollador")]
    [ApiController]
    public class PropertyController : BaseApiController
    {
        [HttpGet("List")]
        [SwaggerOperation(Summary ="Listado de propiedades", Description ="Obtiene todas las propiedades del sistema ")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Get() 
        {
            var properties = await Mediator.Send(new GetAllPropertiestQuery());
            if (properties == null || properties.Count <= 0) 
            { 
                return NoContent();
            }

            return Ok(properties);
        }

        [HttpGet("GetById/{id}")]
        [SwaggerOperation(Summary = "Propiedades por Id", Description = "Obtiene una propiedad utilizando el id de esta ")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Get(int id)
        {
            var property = await Mediator.Send(new GetByIdPropertytQuery() { Id=id});
            if (property == null )
            {
                return NoContent();
            }

            return Ok(property);
        }

        [HttpGet("GetByCode/{code}")]
        [SwaggerOperation(Summary = "Propiedades por el Codigo", Description = "Obtiene una propiedad utilizando el codigo de esta ")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Get(string code)
        {
            var property = await Mediator.Send(new GetByCodePropertytQuery() { Code =  code });
            if (property == null)
            {
                return NoContent();
            }

            return Ok(property);
        }


    }
}
