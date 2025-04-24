using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Features.Account.Commands.RegisterDevelopAsync;
using RealEstateApp.Core.Application.Features.Account.Queries.AuthenticateAsync;
using RealEstateApp.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RealEstateApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Registro y login de usuario")]
    [ApiController]
    public class AccountController : BaseApiController
    {

        [HttpPost("authenticate")]
        [SwaggerOperation(Summary ="Login de usuario", Description ="Autentica un usuario en el sistema y le retorna un JWT")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> AuthenticateAsync(string EmailOrUsername, string Password)
        {
            var user = await Mediator.Send(new AuthenticateAsyncQuery { EmailOrUsername= EmailOrUsername, Password= Password });
            if (user == null) 
            {
                return BadRequest();
            }

            return Ok(user);
        }


        [HttpPost("register/developer")]
        [SwaggerOperation(Summary = "Registro de usuario tipo desarrollador", Description = "Recibe los parametros necesarios para crear un usuario con el rol desarrollador")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> RegisterDeveloperAsync([FromBody] RegisterDeveloperUserAsyncCommand command)
        {

            var user = await Mediator.Send(command);
            return NoContent();
        }

        [HttpPost("register/admin")]
        [SwaggerOperation(Summary = "Registro de usuario tipo administrador", Description = "Recibe los parametros necesarios para crear un usuario con el rol administrador")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> RegisterAdminAsync([FromBody] RegisterAdminUserAsyncCommand command)
        {
            var user = await Mediator.Send(command);
            return NoContent();
        }






    }
}
