using MediatR;
using RealEstateApp.Core.Application.Dtos.Property;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;

using System.ComponentModel.DataAnnotations;


namespace RealEstateApp.Core.Application.Features.Properties.Queries.GetByCodeProperty
{
    /// <summary>
    /// Parámetros para buscar por el id de la propiedad
    /// </summary>  
    public class GetByCodePropertytQuery : IRequest<PropertyDto>
    {
        [SwaggerParameter(Description = "Debe colocar el codigo de la propiedad que quiere obtener")]
        [Required]
        public string Code { get; set; }
    }

    public class GetByIdPropertytQueryQueryHandler : IRequestHandler<GetByCodePropertytQuery, PropertyDto> //Se le pasa el request y el dato q maneja la solicitud
    {
        private readonly IPropertyRepository _propertyRepository;


        public GetByIdPropertytQueryQueryHandler(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<PropertyDto> Handle(GetByCodePropertytQuery request, CancellationToken cancellationToken)
        {
            var properties = await GetByCodeProperty(request.Code);
            if (properties == null) return null;
            return properties;

        }


        private async Task<PropertyDto> GetByCodeProperty(string code)
        {
            var properties = await _propertyRepository.GetAllListWithIncludeAsync(new List<string> { "Agent", "PropertyImprovements", "PropertyType", "PropertyImprovements.Improvement" });


            return properties.Where(p => p.Code == code).Select(property => new PropertyDto
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
            }).FirstOrDefault();
        }
    }




}
