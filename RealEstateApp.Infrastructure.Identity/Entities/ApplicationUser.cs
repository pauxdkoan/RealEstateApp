
using Microsoft.AspNetCore.Identity;

namespace RealEstateApp.Infrastructure.Identity.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string? Photo { get; set; }
        public bool IsActive {  get; set; }


        /*Esto lo puse nulleable por q 
         *solo los desarrolladores y los 
         *admin lo usan*/
        public string? IdentityCard { get; set; } //Cedula
    }
}
