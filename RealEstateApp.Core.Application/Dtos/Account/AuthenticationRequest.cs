

namespace RealEstateApp.Core.Application.Dtos.Account
{
    public class AuthenticationRequest
    {
        public string EmailOrUsername {  get; set; }
        public string Password { get; set; }
    }
}
