using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Exceptions;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealEstateApp.Core.Application.Features.SaleType.Commands.UpdateSaleType
{
    /// <summary>
    /// Parámetros para la actualizacion de un tipo de venta
    /// </summary> 
    public class UpdateSaleTypeCommand : IRequest<SaleTypeUpdateResponse>
    {
        [SwaggerParameter(Description = "El id del tipo de la propiedad que se quiere actualizar")]
        public int? Id { get; set; }

        /// <example>Alquiler Amueblado</example>
        [SwaggerParameter(Description = "El nuevo nombre del tipo de venta")]
        public string? Name { get; set; }

        /// <example>Alquiler con immuebles incluidos</example>
        [SwaggerParameter(Description = "La nueva descripcion del tipo de venta")]
        public string? Description { get; set; }
    }
    public class UpdateSaleTypeeCommandHandler : IRequestHandler<UpdateSaleTypeCommand, SaleTypeUpdateResponse>
    {
        private readonly ISalesTypeRepository _salesTypeRepository;
        private readonly IMapper _mapper;

        public UpdateSaleTypeeCommandHandler(ISalesTypeRepository salesTypeRepository, IMapper mapper)
        {
            _salesTypeRepository = salesTypeRepository;
            _mapper = mapper;
        }
        public async Task<SaleTypeUpdateResponse> Handle(UpdateSaleTypeCommand command, CancellationToken cancellationToken)
        {
            var saleType = await _salesTypeRepository.GetByIdAsync(command.Id.Value);

            if (saleType == null)
            {
                throw new ApiException($"Tipo de venta no encontrada.", (int)HttpStatusCode.BadRequest);
            }
            else
            {
                saleType = _mapper.Map<SalesType>(command);
                await _salesTypeRepository.UpdateAsync(saleType, saleType.Id);
                var saleTypeVm = _mapper.Map<SaleTypeUpdateResponse>(saleType);

                return saleTypeVm;
            }
        }
    }
}
