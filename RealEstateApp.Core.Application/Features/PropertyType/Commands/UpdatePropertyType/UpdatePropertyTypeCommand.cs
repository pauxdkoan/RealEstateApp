using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Exceptions;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealEstateApp.Core.Application.Features.PropertyType.Commands.UpdatePropertyType
{
    /// <summary>
    /// Parámetros para la actualizacion de un tipo de propiedad
    /// </summary> 
    public class UpdatePropertyTypeCommand : IRequest<PropertyTypeUpdateResponse>
    {
        [SwaggerParameter(Description = "El id del tipo de la propiedad que se quiere actualizar")]
        public int? Id { get; set; }

        /// <example>Casa</example>
        [SwaggerParameter(Description = "El nuevo nombre del tipo de la propiedad")]
        public string? Name { get; set; }

        /// <example>Casa confortable para vivir</example>
        [SwaggerParameter(Description = "La nueva descripcion del tipo de la propiedad")]
        public string? Description { get; set; }
    }
    public class UpdatePropertyTypeCommandHandler : IRequestHandler<UpdatePropertyTypeCommand, PropertyTypeUpdateResponse>
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        private readonly IMapper _mapper;

        public UpdatePropertyTypeCommandHandler(IPropertyTypeRepository propertyTypeRepository, IMapper mapper)
        {
            _propertyTypeRepository = propertyTypeRepository;
            _mapper = mapper;
        }
        public async Task<PropertyTypeUpdateResponse> Handle(UpdatePropertyTypeCommand command, CancellationToken cancellationToken)
        {
            var property = await _propertyTypeRepository.GetByIdAsync(command.Id.Value);

            if (property == null)
            {
                throw new ApiException($"Tipo de propiedad no encontrada.", (int)HttpStatusCode.BadRequest);
            }
            else
            {
                property = _mapper.Map<Domain.Entities.PropertyType>(command);
                await _propertyTypeRepository.UpdateAsync(property, property.Id);
                var propertyVm = _mapper.Map<PropertyTypeUpdateResponse>(property);

                return propertyVm;
            }
        }
    }
}
