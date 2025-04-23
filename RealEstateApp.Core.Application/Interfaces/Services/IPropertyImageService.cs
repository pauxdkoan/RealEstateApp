

using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.Property.PropertyImage;
using RealEstateApp.Core.Application.ViewModels.Property.PropertyType;
using RealEstateApp.Core.Application.ViewModels.SalesType;
using RealEstateApp.Core.Domain.Entities;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IPropertyImageService : IGenericService<SavePropertyImageVm,PropertyImageVm, PropertyImage, int>
    {


    }
}
