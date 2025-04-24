using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Exceptions;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealEstateApp.Core.Application.Features.Improvement.Commands.UpdateImprovement
{
    /// <summary>
    /// Parámetros para la actualizacion de una mejora
    /// </summary> 
    public class UpdateImprovementCommand : IRequest<ImprovementUpdateResponse>
    {
        ///<example>1</example>

        [SwaggerParameter(Description = "El id de la mejora que se quiere actualizar")]
        public int? Id { get; set; }

        ///<example>Balcón</example>
        [SwaggerParameter(Description = "El nuevo nombre de la mejora ")]
        public string? Name { get; set; }

        /// <example>Esta mejora incluye Balcón con vista hermosa</example>
        [SwaggerParameter(Description = "La nueva descripcion la mejora")]
        public string? Description { get; set; }
    }
    public class UpdateImprovemenCommandHandler : IRequestHandler<UpdateImprovementCommand, ImprovementUpdateResponse>
    {
        private readonly IImprovementRepository _improvementRepository;
        private readonly IMapper _mapper;

        public UpdateImprovemenCommandHandler(IImprovementRepository improvementRepository, IMapper mapper)
        {
            _improvementRepository = improvementRepository;
            _mapper = mapper;
        }
        public async Task<ImprovementUpdateResponse> Handle(UpdateImprovementCommand command, CancellationToken cancellationToken)
        {
            var improvement = await _improvementRepository.GetByIdAsync(command.Id.Value);

            if (improvement == null)
            {
                throw new ApiException($"Mejora no encontrada.", (int)HttpStatusCode.BadRequest);
            }
            else
            {
                improvement = _mapper.Map<Domain.Entities.Improvement>(command);
                await _improvementRepository.UpdateAsync(improvement, improvement.Id);
                var improvementVm = _mapper.Map<ImprovementUpdateResponse>(improvement);

                return improvementVm;
            }
        }
    }
}
