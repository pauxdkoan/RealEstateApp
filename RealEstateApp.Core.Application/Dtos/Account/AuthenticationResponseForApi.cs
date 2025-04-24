

using System.Text.Json.Serialization;

namespace RealEstateApp.Core.Application.Dtos.Account
{
    public class AuthenticationResponseForApi
    {
        public string Id {  get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email {  get; set; }
        public string IdentityCard { get; set; } //Cedula

        public List<string> Roles { get; set; }
        public bool IsVerified {  get; set; }
        public bool HasError {  get; set; }
        public string? Error { get; set; }

        public string? JWToken { get; set; }

        [JsonIgnore]
        public string? RefreshToken { get; set; }

    }
}
