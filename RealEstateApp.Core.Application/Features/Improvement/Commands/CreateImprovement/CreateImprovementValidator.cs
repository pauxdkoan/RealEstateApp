using FluentValidation;

namespace RealEstateApp.Core.Application.Features.Improvement.Commands.CreateImprovement
{
    public class CreateImprovementValidator : AbstractValidator<CreateImprovementCommand>
    {
        public CreateImprovementValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("El nombre de la mejora es requerido");

            RuleFor(p => p.Description)
              .NotEmpty().WithMessage("La descripcion de la mejora es requerido");



        }
    }
}
