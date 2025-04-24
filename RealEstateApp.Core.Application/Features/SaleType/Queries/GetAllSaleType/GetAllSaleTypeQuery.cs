using MediatR;
using RealEstateApp.Core.Application.Dtos.PropertyType;
using RealEstateApp.Core.Application.Dtos.SaleType;
using RealEstateApp.Core.Application.Interfaces.Repositories;


namespace RealEstateApp.Core.Application.Features.SaleType.Queries.GetAllSaleType
{
    /// <summary>
    /// Se obtiene todos los parametros 
    /// </summary>
    public class GetAllSaleTypeQuery : IRequest<IList<SalesTypeDto>>
    {

    }

    public class GetAllSaleTypeQueryHandler : IRequestHandler<GetAllSaleTypeQuery, IList<SalesTypeDto>> //Se le pasa el request y el dato q maneja la solicitud
    {
        private readonly ISalesTypeRepository _salesTypeRepository;



        public GetAllSaleTypeQueryHandler(ISalesTypeRepository salesTypeRepository)
        {
            _salesTypeRepository = salesTypeRepository;


        }

        public async Task<IList<SalesTypeDto>> Handle(GetAllSaleTypeQuery request, CancellationToken cancellationToken)
        {
            var saleTypes = await GetAllSaleType();
            return saleTypes;

        }



        private async Task<List<SalesTypeDto>> GetAllSaleType()
        {
            var saleTypes = await _salesTypeRepository.GetAllListAsync();


            return saleTypes.Select(p => new SalesTypeDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
            }).ToList();
        }
    }




}
