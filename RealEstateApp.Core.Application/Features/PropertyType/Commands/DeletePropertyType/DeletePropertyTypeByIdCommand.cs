using MediatR;
using RealEstateApp.Core.Application.Exceptions;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealEstateApp.Core.Application.Features.PropertyType.Commands.DeletePropertyType
{
    /// <summary>
    /// Parámetros para la eliminacion de un tipo de propiedad
    /// </summary> 
    public class DeletePropertyTypeByIdCommand : IRequest<int>
    {
        [SwaggerParameter(Description = "El id del tipo de propiedad que se desea eliminar")]
        public int Id { get; set; }
    }

    public class DeletePropertyTypeByIdCommandHandler : IRequestHandler<DeletePropertyTypeByIdCommand, int>
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        public DeletePropertyTypeByIdCommandHandler(IPropertyTypeRepository propertyTypeRepository)
        {
            _propertyTypeRepository = propertyTypeRepository;
        }
        public async Task<int> Handle(DeletePropertyTypeByIdCommand command, CancellationToken cancellationToken)
        {
            var propertyType = await _propertyTypeRepository.GetByIdAsync(command.Id);

            if (propertyType == null) throw new ApiException($"Tipo de propiedad no encontrada.", (int)HttpStatusCode.BadRequest);

            await _propertyTypeRepository.DeleteAsync(propertyType);

            return propertyType.Id;
        }
    }
}
