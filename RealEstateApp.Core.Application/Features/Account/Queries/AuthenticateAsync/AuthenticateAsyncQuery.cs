

using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.Account.Queries.AuthenticateAsync
{
    /// <summary>
    /// Propiedades necesarias para authenticarse
    /// </summary>
    public class AuthenticateAsyncQuery:IRequest<AuthenticationResponseForApi>
    {
        [SwaggerParameter(Description = "El nombre del usuario o correo de este")]
        public string EmailOrUsername { get; set; }
           
        [SwaggerParameter(Description = "La contraseña del usuario")]
        public string Password { get; set; }


    }

    public class AuthenticateAsyncQueryHandler : IRequestHandler<AuthenticateAsyncQuery, AuthenticationResponseForApi>
    {
        private readonly IWebApiAccountService _webApiAccountService;
        private readonly IMapper _mapper;


        public AuthenticateAsyncQueryHandler(IWebApiAccountService webApiAccountService, IMapper mapper ) 
        {
            _webApiAccountService= webApiAccountService;
            _mapper= mapper;
        }

        public Task<AuthenticationResponseForApi> Handle(AuthenticateAsyncQuery request, CancellationToken cancellationToken)
        {
            return _webApiAccountService.AuthenticateAsync(_mapper.Map<AuthenticationRequest>(request));
        }


    }
}
