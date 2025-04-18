

using RealEstateApp.Core.Application.ViewModels.Property.FavoriteProperty;
using RealEstateApp.Core.Application.ViewModels.Property.PropertyImage;
using RealEstateApp.Core.Application.ViewModels.Property.PropertyType;
using RealEstateApp.Core.Application.ViewModels.SalesType;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.ViewModels.Property
{
    public class PropertyVm
    {
        public int Id {  get; set; }
        public string Code {  get; set; } //Id de 6 digitos unico
        public decimal Price {  get; set; }

        public int AmountOfRoom { get; set; }
        public int AmountOfBathroom { get; set; }
        public double PropertySize { get; set; } //En metros

        //RELACIONES:
        //Nota faltan: si es necesario agreguenlas 

        public ICollection<PropertyImageVm> PropertyImageVms { get; set; } //SOLO 1 IMAGEN TOMAR EN LA Q EL ORDER E 1

        public int PropertyTypeId {  get; set; } //Apartamento, casa, etc
        public PropertyTypeVm PropertyTypeVm { get; set; }

        public int SalesTypeId {  get; set; } //Aquiler, venta, etc
        public SalesTypeVm SalesTypeVm { get; set; }

        public ICollection<FavoritePropertyVm> FavoritePropertyVms { get; set; }




    }
}
