using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Account.Commands.RegisterDevelopAsync
{
    public class RegisterDeveloperValidator : AbstractValidator<RegisterDeveloperUserAsyncCommand>
    {
        public RegisterDeveloperValidator()
        {
            RuleFor(p => p.Email)
             .NotEmpty().WithMessage("El correo es requerido");

            RuleFor(p => p.Password)
              .NotEmpty().WithMessage("La contraseña del usuario es requerida");

            RuleFor(p => p.FirstName)
            .NotEmpty().WithMessage("El nombre del usuario es requerida");

            RuleFor(p => p.LastName)
           .NotEmpty().WithMessage("El apellido del usuario es requerida");

            RuleFor(p => p.IdentityCard)
             .NotEmpty().WithMessage("La cedula del usuario es requerida");

        }
    }
}
