using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.Improvement;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;

using System.ComponentModel.DataAnnotations;


namespace RealEstateApp.Core.Application.Features.Improvement.Queries.GetByIdImprovement
{
    /// <summary>
    /// Parámetros para buscar por el id del tipo de la propiedad
    /// </summary>  
    public class GetByIdImprovementQuery : IRequest<ImprovementDto>
    {
        [SwaggerParameter(Description = "Debe colocar el id de la mejora que se quiere obtener")]
        [Required]
        public int Id { get; set; }
    }

    public class GetByIdImprovemeneQueryHandler : IRequestHandler<GetByIdImprovementQuery, ImprovementDto> //Se le pasa el request y el dato q maneja la solicitud
    {
        private readonly IImprovementRepository _improvementRepository;
        private readonly IMapper _mapper;


        public GetByIdImprovemeneQueryHandler(IImprovementRepository improvementRepository, IMapper mapper)
        {
            _improvementRepository = improvementRepository;
            _mapper = mapper;


        }

        public async Task<ImprovementDto> Handle(GetByIdImprovementQuery request, CancellationToken cancellationToken)
        {
            var improvement = await GetByIdImprovement(request.Id);
            if (improvement == null) return null;
            return improvement;

        }



        private async Task<ImprovementDto> GetByIdImprovement(int id)
        {
            var propertyType = await _improvementRepository.GetByIdAsync(id);

            return _mapper.Map<ImprovementDto>(propertyType);


        }
    }




}
