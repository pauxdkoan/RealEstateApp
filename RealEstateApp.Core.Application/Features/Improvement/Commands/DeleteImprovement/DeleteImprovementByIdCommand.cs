using MediatR;
using RealEstateApp.Core.Application.Exceptions;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealEstateApp.Core.Application.Features.Improvement.Commands.DeleteImprovement
{
    /// <summary>
    /// Parámetros para la eliminacion de una mejora
    /// </summary> 
    public class DeleteImprovementByIdCommand : IRequest<int>
    {
        [SwaggerParameter(Description = "El id del tipo de la mejora que se desea eliminar")]
        public int Id { get; set; }
    }

    public class DeleteImprovemeneByIdCommandHandler : IRequestHandler<DeleteImprovementByIdCommand, int>
    {
        private readonly IImprovementRepository _improvementRepository;
        public DeleteImprovemeneByIdCommandHandler(IImprovementRepository improvementRepository)
        {
            _improvementRepository = improvementRepository;
        }
        public async Task<int> Handle(DeleteImprovementByIdCommand command, CancellationToken cancellationToken)
        {
            var improvement = await _improvementRepository.GetByIdAsync(command.Id);

            if (improvement == null) throw new ApiException($"Mejora no encontrada.", (int)HttpStatusCode.BadRequest);

            await _improvementRepository.DeleteAsync(improvement);

            return improvement.Id;
        }
    }
}
