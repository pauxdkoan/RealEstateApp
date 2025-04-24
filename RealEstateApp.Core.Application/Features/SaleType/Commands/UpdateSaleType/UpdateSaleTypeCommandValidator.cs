using FluentValidation;


namespace RealEstateApp.Core.Application.Features.SaleType.Commands.UpdateSaleType
{
    public class UpdateSaleTypeCommandValidator : AbstractValidator<UpdateSaleTypeCommand>
    {
        public UpdateSaleTypeCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("El id del tipo de venta es requerida")
                .GreaterThan(0).WithMessage("El id del tipo de venta debe ser mayor que 0");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("El nombre del tipo de venta es requerida");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("La descripción del tipo de venta es requerida");
        }
    }
}
