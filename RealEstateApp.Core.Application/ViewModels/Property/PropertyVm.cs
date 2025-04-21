

using RealEstateApp.Core.Application.ViewModels.Chat;
using RealEstateApp.Core.Application.ViewModels.Improvement.PropertyImprovement;
using RealEstateApp.Core.Application.ViewModels.Offer;
using RealEstateApp.Core.Application.ViewModels.Property.FavoriteProperty;
using RealEstateApp.Core.Application.ViewModels.Property.PropertyImage;
using RealEstateApp.Core.Application.ViewModels.Property.PropertyType;
using RealEstateApp.Core.Application.ViewModels.SalesType;
using RealEstateApp.Core.Application.ViewModels.User;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.ViewModels.Property
{
    public class PropertyVm
    {
        public int Id {  get; set; }
        public string Code {  get; set; } //Id de 6 digitos unico
        public decimal Price {  get; set; }
        public bool State {  get; set; } //Disponible/Vendidad
        public string Description {  get; set; }
        public int AmountOfRoom { get; set; }
        public int AmountOfBathroom { get; set; }
        public double PropertySize { get; set; } //En metros
        public bool IsFavorite { get; set; }

        //RELACIONES:
        //Nota faltan: si es necesario agreguenlas 



        public int PropertyTypeId {  get; set; } //Apartamento, casa, etc
        public PropertyTypeVm PropertyTypeVm { get; set; }

        public int SalesTypeId {  get; set; } //Aquiler, venta, etc
        public SalesTypeVm SalesTypeVm { get; set; }
        

        public string AgentId {  get; set; }
        public UserVm Agent {  get; set; }


        public ICollection<OfferVm> Offers { get; set; }
        public ICollection<ChatVm> Chats { get; set; }

        public ICollection<FavoritePropertyVm> FavoritePropertyVms { get; set; }

        public ICollection<PropertyImprovementVm> PropertyImprovements {  get; set; }

        public ICollection<PropertyImageVm> PropertyImageVms { get; set; } //SOLO 1 IMAGEN TOMAR EN LA Q EL ORDER E 1




    }
}
