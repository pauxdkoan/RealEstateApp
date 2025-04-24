using FluentValidation;
using RealEstateApp.Core.Application.Features.PropertyType.Commands.UpdatePropertyType;


namespace RealEstateApp.Core.Application.Features.Improvement.Commands.UpdateImprovement
{
    public class UpdateImprovementCommandValidator : AbstractValidator<UpdateImprovementCommand>
    {
        public UpdateImprovementCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("El id la mejora es requerida")
                .GreaterThan(0).WithMessage("El id de la mejora debe ser mayor que 0");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("El nombre  la mejora es requerida");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("La descripción de la mejora es requerida");
        }
    }
}
