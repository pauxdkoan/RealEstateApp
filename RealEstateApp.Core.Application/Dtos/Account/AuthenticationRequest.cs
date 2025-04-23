

namespace RealEstateApp.Core.Application.Dtos.Account
{
    public class UpdateRequest
    {
        public string Id { get; set; }  // Para identificar al usuario a actualizar
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Phone {  get; set; }
        public string? IdentityCard { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string? Password { get; set; } // Opcional: Solo actualizar si se proporciona
        public string Rol { get; set; }  // Para poder actualizar el rol si fuera necesario
        public bool IsActive { get; set; }
    }
}
