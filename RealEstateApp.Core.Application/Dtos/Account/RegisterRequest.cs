
namespace RealEstateApp.Core.Application.Dtos.Account
{
    public class RegisterRequest
    {
        public string? Id {  get; set; }
        public string UserName {  get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Phone {  get; set; }
        public string Password {  get; set; }
        public string? Photo {  get; set; }

        public string? IdentityCard {  get; set; } // admin/desarrolladores

        public string Rol {  get; set; }
    }
}
