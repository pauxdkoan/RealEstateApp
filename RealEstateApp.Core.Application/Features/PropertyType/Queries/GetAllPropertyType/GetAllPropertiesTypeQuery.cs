using MediatR;
using RealEstateApp.Core.Application.Dtos.PropertyType;
using RealEstateApp.Core.Application.Interfaces.Repositories;


namespace RealEstateApp.Core.Application.Features.PropertyType.Queries.GetAllPropertyType
{
    /// <summary>
    /// Se obtiene todos los parametros 
    /// </summary>
    public class GetAllPropertiesTypeQuery : IRequest<IList<PropertyTypeDto>>
    {

    }

    public class GetAllPropertiesTypsQueryHandler : IRequestHandler<GetAllPropertiesTypeQuery, IList<PropertyTypeDto>> //Se le pasa el request y el dato q maneja la solicitud
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;



        public GetAllPropertiesTypsQueryHandler(IPropertyTypeRepository propertyRepository)
        {
            _propertyTypeRepository = propertyRepository;


        }

        public async Task<IList<PropertyTypeDto>> Handle(GetAllPropertiesTypeQuery request, CancellationToken cancellationToken)
        {
            var properties = await GetAllPropertiesType();
            return properties;

        }



        private async Task<List<PropertyTypeDto>> GetAllPropertiesType()
        {
            var propertiesType = await _propertyTypeRepository.GetAllListAsync();


            return propertiesType.Select(p => new PropertyTypeDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
            }).ToList();
        }
    }




}
