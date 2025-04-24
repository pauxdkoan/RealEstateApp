using FluentValidation;

namespace RealEstateApp.Core.Application.Features.SaleType.Commands.CreateSaleType
{
    public class CreateSaleTypeCommandValidator : AbstractValidator<CreateSaleTypeCommand>
    {
        public CreateSaleTypeCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("El nombre del tipo de venta es requerido");

            RuleFor(p => p.Description)
              .NotEmpty().WithMessage("La descripcion del tipo de venta es requerido");



        }
    }
}
