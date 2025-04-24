using FluentValidation;


namespace RealEstateApp.Core.Application.Features.PropertyType.Commands.UpdatePropertyType
{
    public class UpdatePropertyTypeCommandValidator : AbstractValidator<UpdatePropertyTypeCommand>
    {
        public UpdatePropertyTypeCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("El id del tipo de la propiedad es requerida")
                .GreaterThan(0).WithMessage("El id del tipo de la propiedad debe ser mayor que 0");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("El nombre del tipo de la propiedad es requerida");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("La descripción del tipo de la propiedad es requerida");
        }
    }
}
