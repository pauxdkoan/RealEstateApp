

using RealEstateApp.Core.Application.ViewModels.Offer;
using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.ViewModels.User
{
    public class UserVm
    {
        public string Id {  get; set; }
        public string FirstName {  get; set; }
        public string UserName {  get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Phone {  get; set; }
        public string? Photo {  get; set; }
        public string Rol {  get; set; }
        public bool IsActive {  get; set; }
        public string? IdentityCard {  get; set; }


        /*CUANDO SE CREEN LOS VM AGREGUEN LOS COLLECTION:*/

        //public ICollection<PropertyVm> Properties { get; set; }
        //public ICollection<OfferVm> Offers { get; set; }
        //public ICollection<> FavoriteProperties { get; set; }
    }
}
