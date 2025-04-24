using MediatR;
using RealEstateApp.Core.Application.Dtos.Improvement;
using RealEstateApp.Core.Application.Interfaces.Repositories;


namespace RealEstateApp.Core.Application.Features.Improvement.Queries.GetAllImprovement
{
    /// <summary>
    /// Se obtiene todos los parametros 
    /// </summary>
    public class GetAllImprovementQuery : IRequest<IList<ImprovementDto>>
    {

    }

    public class GetAllImprovementQueryHandler : IRequestHandler<GetAllImprovementQuery, IList<ImprovementDto>> //Se le pasa el request y el dato q maneja la solicitud
    {
        private readonly IImprovementRepository _improvementRepository;



        public GetAllImprovementQueryHandler(IImprovementRepository improvementRepository)
        {
            _improvementRepository = improvementRepository;


        }

        public async Task<IList<ImprovementDto>> Handle(GetAllImprovementQuery request, CancellationToken cancellationToken)
        {
            var improvements = await GetAllImprovement();
            return improvements;

        }



        private async Task<List<ImprovementDto>> GetAllImprovement()
        {
            var improvements = await _improvementRepository.GetAllListAsync();


            return improvements.Select(p => new ImprovementDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
            }).ToList();
        }
    }




}
