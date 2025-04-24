

using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.SaleType.Commands.CreateSaleType
{
    /// <summary>
    /// Parámetros para la creacion de un tipo de venta
    /// </summary>  
    public class CreateSaleTypeCommand : IRequest<int>
    {
        /// <example>Alquiler Amueblado</example>
        [SwaggerParameter(Description = "El nombre del tipo de venta")]
        public string? Name { get; set; }

        /// <example>Aquiler con imueble incluido</example>
        [SwaggerParameter(Description = "La descripcion del tipo de venta")]
        public string? Description { get; set; }
    }
    public class CreateSaleTypeCommandHandler : IRequestHandler<CreateSaleTypeCommand, int>
    {
        private readonly ISalesTypeRepository _salesTypeRepository;
        private readonly IMapper _mapper;
        public CreateSaleTypeCommandHandler(ISalesTypeRepository salesTypeRepository, IMapper mapper)
        {
            _salesTypeRepository = salesTypeRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateSaleTypeCommand request, CancellationToken cancellationToken)
        {
            var saleType = _mapper.Map<Domain.Entities.SalesType>(request);
            await _salesTypeRepository.AddAsync(saleType);
            return saleType.Id;
        }
    }
}
