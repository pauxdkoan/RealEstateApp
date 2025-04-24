using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Account.Queries.AuthenticateAsync
{
    public class AuthenticateAsyncValidator:AbstractValidator<AuthenticateAsyncQuery>
    {
        public AuthenticateAsyncValidator() 
        {
            RuleFor(p => p.EmailOrUsername)
             .NotEmpty().WithMessage("El nombre de usuario o correo es requerido");

            RuleFor(p => p.Password)
              .NotEmpty().WithMessage("La contraseña del usuario es requerida");



        }
    }
}
