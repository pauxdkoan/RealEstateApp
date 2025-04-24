using MediatR;
using RealEstateApp.Core.Application.Dtos.Agent;
using RealEstateApp.Core.Application.Dtos.Property;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;

using System.ComponentModel.DataAnnotations;


namespace RealEstateApp.Core.Application.Features.Properties.Queries.GetByIdProperty
{
    /// <summary>
    /// Parámetros para buscar por el id del agente
    /// </summary>  
    public class GetByIdAgentQuery : IRequest<AgentDto>
    {
        [SwaggerParameter(Description = "Debe colocar el id del agente que quiere obtener los datos")]
        [Required]
        public string Id { get; set; }
    }

    public class GetByIdAgentQueryQueryHandler : IRequestHandler<GetByIdAgentQuery, AgentDto> //Se le pasa el request y el dato q maneja la solicitud
    {
        private readonly IUserRepository _userRepository;



        public GetByIdAgentQueryQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;


        }

        public async Task<AgentDto> Handle(GetByIdAgentQuery request, CancellationToken cancellationToken)
        {
            var agent = await GetByIdAgent(request.Id);
            if (agent == null) return null;
            return agent;
        }



        private async Task<AgentDto> GetByIdAgent(string id)
        {
            var agents = await _userRepository.GetAllListWithIncludeAsync(new List<string> { "Properties" });


            return agents.Where(a => a.Id == id).Select(a => new AgentDto
            {
                Id = a.Id,
                AmountOfProperty = a.Properties.Count(),
                Email=a.Email,
                FirstName=a.FirstName,
                LastName=a.LastName,
                Phone=a.Phone

            }).FirstOrDefault();             
        }
    }




}
