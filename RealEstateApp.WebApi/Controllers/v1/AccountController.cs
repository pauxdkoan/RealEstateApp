using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealEstateApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Registro y login de usuario")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IWebApiAccountService _accountService;
        public AccountController(IWebApiAccountService accountService) 
        { 
            _accountService = accountService;
        }


        [HttpPost("authenticate")]
        [SwaggerOperation(Summary ="Login de usuario", Description ="Autentica un usuario en el sistema y le retorna un JWT")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            var xd = await _accountService.AuthenticateAsync(request);
            return Ok(await _accountService.AuthenticateAsync(request));
        }


        [HttpPost("register/developer")]
        [SwaggerOperation(Summary = "Registro de usuario tipo desarrollador", Description = "Recibe los parametros necesarios para crear un usuario con el rol desarrollador")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> RegisterDeveloperAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterUserAsync(request, origin));
        }

        [HttpPost("register/admin")]
        [SwaggerOperation(Summary = "Registro de usuario tipo administrador", Description = "Recibe los parametros necesarios para crear un usuario con el rol administrador")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> RegisterAdminAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterUserAsync(request, origin));
        }






    }
}
