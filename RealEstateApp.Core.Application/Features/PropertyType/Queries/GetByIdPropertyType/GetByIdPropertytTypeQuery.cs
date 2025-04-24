using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.Property;
using RealEstateApp.Core.Application.Dtos.PropertyType;
using RealEstateApp.Core.Application.Features.Properties.Queries.GetByIdProperty;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;

using System.ComponentModel.DataAnnotations;


namespace RealEstateApp.Core.Application.Features.PropertyType.Queries.GetByIdPropertyType
{
    /// <summary>
    /// Parámetros para buscar por el id del tipo de la propiedad
    /// </summary>  
    public class GetByIdPropertytTypeQuery : IRequest<PropertyTypeDto>
    {
        [SwaggerParameter(Description = "Debe colocar el id del tipo de la propiedad que quiere obtener")]
        [Required]
        public int Id { get; set; }
    }

    public class GetByIdPropertytTypeQueryHandler : IRequestHandler<GetByIdPropertytTypeQuery, PropertyTypeDto> //Se le pasa el request y el dato q maneja la solicitud
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        private readonly IMapper _mapper;


        public GetByIdPropertytTypeQueryHandler(IPropertyTypeRepository propertyTypeRepository, IMapper mapper)
    {
            _propertyTypeRepository = propertyTypeRepository;
            _mapper=mapper;


    }

    public async Task<PropertyTypeDto> Handle(GetByIdPropertytTypeQuery request, CancellationToken cancellationToken)
    {
        var properties = await GetByIdPropertyType(request.Id);
        if (properties == null) return null;
        return properties;

    }



    private async Task<PropertyTypeDto> GetByIdPropertyType(int id)
    {
        var propertyType = await _propertyTypeRepository.GetByIdAsync(id);

            return _mapper.Map<PropertyTypeDto>(propertyType);


    }
}




}
