using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using RealEstateApp.Core.Application.Dtos.SaleType;
using RealEstateApp.Core.Application.Features.SaleType.Commands.CreateSaleType;
using RealEstateApp.Core.Application.Features.SaleType.Commands.DeleteSaleType;
using RealEstateApp.Core.Application.Features.SaleType.Commands.UpdateSaleType;
using RealEstateApp.Core.Application.Features.SaleType.Queries.GetAllSaleType;
using RealEstateApp.Core.Application.Features.SaleType.Queries.GetByIdSaleType;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealEstateApp.WebApi.Controllers.v1
{

    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de  tipo de ventas")]


    [ApiController]
    public class SaleTypeController : BaseApiController
    {

        [Authorize(Roles = "Administrador")]
        [HttpPost("Create")]
        [SwaggerOperation( Summary = "Creacion de tipo de venta",Description = "Recibe los parametros necesarios para crear una nuevo tipo de venta")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreateSaleTypeCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("Update/{id}")]
        [SwaggerOperation(Summary = "Actualizacion de tipo de venta", Description = "Recibe los parametros necesarios para modificar un tipo de venta existente")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaleTypeUpdateResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateSaleTypeCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("Delete/{id}")]
        [SwaggerOperation(Summary = "Eliminar un tipo de venta",Description = "Recibe los parametros necesarios para eliminar un tipo de venta existente, al eliminar un tipo de venta " +"se elimina las propiedades asociada con esta")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteSaleTypeByIdCommand { Id = id });
            return NoContent();
        }

        [Authorize(Roles = "Administrador,Desarrollador")]
        [HttpGet("List")]
        [SwaggerOperation(Summary = "Listado de tipos de venta", Description = "Obtiene todas los tipos de venta del sistema ")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SalesTypeDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Get()
        {
            var saleTypes = await Mediator.Send(new GetAllSaleTypeQuery());
            if (saleTypes == null || saleTypes.Count <= 0)
            {
                return NoContent();
            }

            return Ok(saleTypes);
        }

        [Authorize(Roles = "Administrador,Desarrollador")]
        [HttpGet("GetById/{id}")]
        [SwaggerOperation(Summary = "Tipos de venta por Id", Description = "Obtiene un tipo de venta utilizando el id de esta ")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SalesTypeDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Get(int id)
        {
            var saleType = await Mediator.Send(new GetByIdSaleTypeQuery() { Id = id });
            if (saleType == null)
            {
                return NoContent();
            }

            return Ok(saleType);
        }

    }
}
