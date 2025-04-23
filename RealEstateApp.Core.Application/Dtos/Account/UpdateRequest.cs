

namespace RealEstateApp.Core.Application.Dtos.Account
{
    public class AuthenticationRequest
    {
        public string EmailOrUsername {  get; set; } //Se indica q puede ser username o email elegi email
        public string Password { get; set; }
    }
}
