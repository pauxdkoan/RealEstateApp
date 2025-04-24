using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.Property;
using RealEstateApp.Core.Application.Dtos.PropertyType;
using RealEstateApp.Core.Application.Dtos.SaleType;
using RealEstateApp.Core.Application.Features.Properties.Queries.GetByIdProperty;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;

using System.ComponentModel.DataAnnotations;


namespace RealEstateApp.Core.Application.Features.SaleType.Queries.GetByIdSaleType
{
    /// <summary>
    /// Parámetros para buscar por el id del tipo de venta
    /// </summary>  
    public class GetByIdSaleTypeQuery : IRequest<SalesTypeDto>
    {
        [SwaggerParameter(Description = "Debe colocar el id del tipo de venta que quiere obtener")]
        [Required]
        public int Id { get; set; }
    }

    public class GetByIdSaleTypeQueryHandler : IRequestHandler<GetByIdSaleTypeQuery, SalesTypeDto> //Se le pasa el request y el dato q maneja la solicitud
    {
        private readonly ISalesTypeRepository _salesTypeRepository;
        private readonly IMapper _mapper;


        public GetByIdSaleTypeQueryHandler(ISalesTypeRepository salesTypeRepository, IMapper mapper)
        {
            _salesTypeRepository = salesTypeRepository;
            _mapper = mapper;


        }

        public async Task<SalesTypeDto> Handle(GetByIdSaleTypeQuery request, CancellationToken cancellationToken)
        {
            var saleType = await GetByIdSaleType(request.Id);
            if (saleType == null) return null;
            return saleType;

        }



        private async Task<SalesTypeDto> GetByIdSaleType(int id)
        {
            var saleType = await _salesTypeRepository.GetByIdAsync(id);

            return _mapper.Map<SalesTypeDto>(saleType);


        }
    }




}
