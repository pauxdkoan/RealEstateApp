using MediatR;
using RealEstateApp.Core.Application.Dtos.Property;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Properties.Queries.GetAllProperties
{
    /// <summary>
    /// Se obtiene todos los parametros 
    /// </summary>
    public class GetAllPropertiestQuery : IRequest<IList<PropertyDto>>
    {

    }

    public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllPropertiestQuery, IList<PropertyDto>> //Se le pasa el request y el dato q maneja la solicitud
    {
        private readonly IPropertyRepository _propertyRepository;
       


        public GetAllPropertiesQueryHandler(IPropertyRepository propertyRepository) 
        { 
            _propertyRepository = propertyRepository;
          

        }

        public async Task<IList<PropertyDto>> Handle(GetAllPropertiestQuery request, CancellationToken cancellationToken)
        {
            var properties = await GetAllProperties();
            return properties;

        }



        private async Task<List<PropertyDto>> GetAllProperties() 
        {
            var properties = await _propertyRepository.GetAllListWithIncludeAsync(new List<string> { "Agent", "PropertyImprovements", "PropertyType", "PropertyImprovements.Improvement" });
            
            
            return properties.Select(property => new PropertyDto
            {
                Id = property.Id, 
                Code = property.Code,
                AgentId = property.AgentId,
                AgentName = property.Agent.FirstName,
                AmountOfBathroom = property.AmountOfBathroom,
                AmountOfRoom = property.AmountOfRoom,
                ImprovementList=property.PropertyImprovements.Select(p=>p.Improvement.Name).ToList(),
                Description=property.Description,
                Price=property.Price,
                PropertySize=property.PropertySize,
                PropertyType=property.PropertyType.Name,
                State= property.State==true?"Disponible":"Vendida"
            }).ToList();
        }
    }

    


}
