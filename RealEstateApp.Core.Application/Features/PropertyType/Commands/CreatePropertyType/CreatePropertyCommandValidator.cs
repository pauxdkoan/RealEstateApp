using FluentValidation;

namespace RealEstateApp.Core.Application.Features.PropertyType.Commands.CreatePropertyType
{
    public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyTypeCommand>
    {
        public CreatePropertyCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("El nombre del tipo de la propiedad es requerido");

            RuleFor(p => p.Description)
              .NotEmpty().WithMessage("La descripcion del tipo de la propiedad es requerido");



        }
    }
}
