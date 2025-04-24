using MediatR;
using RealEstateApp.Core.Application.Dtos.Agent;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Repositories;


namespace RealEstateApp.Core.Application.Features.Agent.Queries.GetAllAgents
{
    /// <summary>
    /// Se obtiene todos los parametros del agente
    /// </summary>
    public class GetAllAgentsQuery : IRequest<IList<AgentDto>>
    {

    }

    public class GetAllAgentsQueryHandler : IRequestHandler<GetAllAgentsQuery, IList<AgentDto>> //Se le pasa el request y el dato q maneja la solicitud
    {
        private readonly IUserRepository _userRepository;



        public GetAllAgentsQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;


        }

        public async Task<IList<AgentDto>> Handle(GetAllAgentsQuery request, CancellationToken cancellationToken)
        {
            var agents = await GetAllAgents();
            return agents;

        }

        private async Task<List<AgentDto>> GetAllAgents()
        {
            var agents = await _userRepository.GetAllListWithIncludeAsync(new List<string> { "Properties" });


            return agents.Where(u => u.Rol == Roles.Agente.ToString()).Select(a => new AgentDto
            {
                Id = a.Id,
                AmountOfProperty = a.Properties.Count(),
                Email = a.Email,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Phone = a.Phone,
            }).ToList();
        }
    }




}
