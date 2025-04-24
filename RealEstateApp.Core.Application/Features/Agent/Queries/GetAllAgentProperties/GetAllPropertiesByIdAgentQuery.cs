using MediatR;
using RealEstateApp.Core.Application.Dtos.Agent;
using RealEstateApp.Core.Application.Dtos.Property;
using RealEstateApp.Core.Application.Features.Properties.Queries.GetByIdProperty;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;

using System.ComponentModel.DataAnnotations;


namespace RealEstateApp.Core.Application.Features.Agent.Queries.GetAllAgentProperties
{
    /// <summary>
    /// Parámetros para buscar la propiedad por el id de agente
    /// </summary>  
    public class GetAllPropertiesByIdAgentQuery : IRequest<List<PropertyDto>>
    {
        [SwaggerParameter(Description = "Debe colocar el id del agente que quiere obtener las propiedades")]
        [Required]
        public string Id { get; set; }
    }

    public class GetAllPropertiesByIdAgenHandler : IRequestHandler<GetAllPropertiesByIdAgentQuery, List<PropertyDto>> //Se le pasa el request y el dato q maneja la solicitud
    {
        private readonly IPropertyRepository _propertyRepository;



        public GetAllPropertiesByIdAgenHandler(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;


        }

        public async Task<List<PropertyDto>> Handle(GetAllPropertiesByIdAgentQuery request, CancellationToken cancellationToken)
        {
            var agent = await GetAllPropertiesByIdAgent(request.Id);
            if (agent == null) return null;
            return agent;
        }



        private async Task<List<PropertyDto>> GetAllPropertiesByIdAgent(string id)
        {
            var properties = await _propertyRepository.GetAllListWithIncludeAsync(new List<string> { "Agent", "PropertyImprovements", "PropertyType", "PropertyImprovements.Improvement" });


            return properties.Where(p=>p.AgentId==id).Select(property => new PropertyDto
            {
                Id = property.Id,
                Code = property.Code,
                AgentId = property.AgentId,
                AgentName = property.Agent.FirstName,
                AmountOfBathroom = property.AmountOfBathroom,
                AmountOfRoom = property.AmountOfRoom,
                ImprovementList = property.PropertyImprovements.Select(p => p.Improvement.Name).ToList(),
                Description = property.Description,
                Price = property.Price,
                PropertySize = property.PropertySize,
                PropertyType = property.PropertyType.Name,
                State = property.State == true ? "Disponible" : "Vendida"
            }).ToList();
        }
    }




}
