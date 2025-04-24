using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Agent;
using RealEstateApp.Core.Application.Dtos.Property;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Features.Agent.Commands.ChangeStatus;
using RealEstateApp.Core.Application.Features.Agent.Queries.GetAllAgentProperties;
using RealEstateApp.Core.Application.Features.Agent.Queries.GetAllAgents;
using RealEstateApp.Core.Application.Features.Properties.Queries.GetByIdProperty;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealEstateApp.WebApi.Controllers.v1
{
   
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de agentes")]


    [ApiController]
    public class AgentController : BaseApiController
    {
     
        
        [HttpGet("List")]
        [SwaggerOperation(Summary ="Listado de agentes", Description ="Obtiene todos los agentes del sistema ")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgentDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Get() 
        {
            var agents = await Mediator.Send(new GetAllAgentsQuery());
            if (agents == null || agents.Count <= 0) 
            { 
                return NoContent();
            }

            return Ok(agents);
        }
        [Authorize(Roles = "Administrador,Desarrollador")]
        [HttpGet("GetById/{id}")]
        [SwaggerOperation(Summary = "Agentes por Id", Description = "Obtiene un Agente utilizando el id de esta ")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgentDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Get(string id)
        {
            var agent = await Mediator.Send(new GetByIdAgentQuery() { Id=id});
            if (agent == null )
            {
                return NoContent();
            }

            return Ok(agent);
        }

        [Authorize(Roles = "Administrador,Desarrollador")]

        [HttpGet("GetAgentProperty/{id}")]
        [SwaggerOperation(Summary = "Propiedades por Id del agente", Description = "Obtiene los datos de las propiedades del agente utilizando el id de este ")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetAllPropertiesByIdAgent(string id)
        {
            var agent = await Mediator.Send(new GetAllPropertiesByIdAgentQuery() { Id = id });
            if (agent == null)
            {
                return NoContent();
            }
            return Ok(agent);
        }


        [Authorize(Roles = "Administrador")]

        [HttpGet("ChangeStatus/{id}/{status}")]
        [SwaggerOperation(Summary = "Atualiza el estado del agente", Description = "Se cambia el estado del agente mediante su id y con el estado deseado ")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> ChangeStatusAgent(string id, bool status)
        {
            await Mediator.Send(new ChangeStatusAgentCommand() { Id = id, Status=status });
            return NoContent();
            
        }





    }
}
