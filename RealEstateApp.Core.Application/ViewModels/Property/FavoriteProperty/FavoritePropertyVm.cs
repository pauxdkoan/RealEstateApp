

using RealEstateApp.Core.Application.ViewModels.User.Client;

namespace RealEstateApp.Core.Application.ViewModels.Property.FavoriteProperty
{
    public class FavoritePropertyVm
    {
        public int Id { get; set; }
        public string ClientId {  get; set; }
        public ClientVm Client { get; set; }

        public int PropertyId {  get; set; }
        public PropertyVm Property { get; set; }
    }
}
