using MediatR;
using RealEstateApp.Core.Application.Exceptions;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealEstateApp.Core.Application.Features.SaleType.Commands.DeleteSaleType
{
    /// <summary>
    /// Parámetros para la eliminacion de un tipo de venta
    /// </summary> 
    public class DeleteSaleTypeByIdCommand : IRequest<int>
    {
        [SwaggerParameter(Description = "El id del tipo de venta que se desea eliminar")]
        public int Id { get; set; }
    }

    public class DeleteSaleTypeByIdCommandHandler : IRequestHandler<DeleteSaleTypeByIdCommand, int>
    {
        private readonly ISalesTypeRepository _salesTypeRepository;
        public DeleteSaleTypeByIdCommandHandler(ISalesTypeRepository salesTypeRepository)
        {
            _salesTypeRepository = salesTypeRepository;
        }
        public async Task<int> Handle(DeleteSaleTypeByIdCommand command, CancellationToken cancellationToken)
        {
            var saleType = await _salesTypeRepository.GetByIdAsync(command.Id);

            if (saleType == null) throw new ApiException($"Tipo de venta no encontrada.", (int)HttpStatusCode.BadRequest);

            await _salesTypeRepository.DeleteAsync(saleType);

            return saleType.Id;
        }
    }
}
