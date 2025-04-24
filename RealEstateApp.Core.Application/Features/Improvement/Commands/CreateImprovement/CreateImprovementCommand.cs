

using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Features.PropertyType.Commands.CreatePropertyType;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.Improvement.Commands.CreateImprovement
{
    /// <summary>
    /// Parámetros para la creacion de una mejora
    /// </summary>  
    public class CreateImprovementCommand : IRequest<int>
    {
        ///<example>Balcón</example>
        [SwaggerParameter(Description = "El nombre de la mejora")]
        public string? Name { get; set; }

        /// <example>Incluye un balcón con buena vista</example>
        [SwaggerParameter(Description = "La descripcion de la mejora")]
        public string? Description { get; set; }
    }
    public class CreateImprovementCommandHandler : IRequestHandler<CreateImprovementCommand, int>
    {
        private readonly IImprovementRepository _improvementRepository;
        private readonly IMapper _mapper;
        public CreateImprovementCommandHandler(IImprovementRepository improvementRepository, IMapper mapper)
        {
            _improvementRepository = improvementRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateImprovementCommand request, CancellationToken cancellationToken)
        {
            var improvement = _mapper.Map<Domain.Entities.Improvement>(request);
            await _improvementRepository.AddAsync(improvement);
            return improvement.Id;
        }
    }
}
