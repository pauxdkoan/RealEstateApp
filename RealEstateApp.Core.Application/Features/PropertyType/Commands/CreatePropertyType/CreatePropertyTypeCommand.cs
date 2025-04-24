

using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.PropertyType.Commands.CreatePropertyType
{
    /// <summary>
    /// Parámetros para la creacion de un tipo de propiedad
    /// </summary>  
    public class CreatePropertyTypeCommand : IRequest<int>
    {
        /// <example>Apartamento</example>
        [SwaggerParameter(Description = "El nombre del tipo de propiedad")]
        public string? Name { get; set; }

        /// <example>Apartamento confortable para vivir</example>
        [SwaggerParameter(Description = "La descripcion del tipo de propiedad")]
        public string? Description { get; set; }
    }
    public class CreatePropertyTypeCommandHandler : IRequestHandler<CreatePropertyTypeCommand, int>
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        private readonly IMapper _mapper;
        public CreatePropertyTypeCommandHandler(IPropertyTypeRepository propertyTypeRepository, IMapper mapper)
        {
            _propertyTypeRepository = propertyTypeRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreatePropertyTypeCommand request, CancellationToken cancellationToken)
        {
            var propertyType = _mapper.Map<Domain.Entities.PropertyType>(request);
            await _propertyTypeRepository.AddAsync(propertyType);
            return propertyType.Id;
        }
    }
}
