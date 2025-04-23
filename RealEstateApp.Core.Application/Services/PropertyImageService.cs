

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Property.PropertyImage;
using RealEstateApp.Core.Application.ViewModels.SalesType;
using RealEstateApp.Core.Domain.Entities;


namespace RealEstateApp.Core.Application.Services
{
    public class PropertyImageService : GenericService<SavePropertyImageVm, PropertyImageVm, PropertyImage, int>,IPropertyImageService
    {
        
        private readonly IMapper _mapper;
        private readonly IPropertyImageRepository _propertyImageRepository;
  

        public PropertyImageService( IMapper mapper, IPropertyImageRepository propertyImageRepository ) :base(propertyImageRepository, mapper)
        {
            _mapper = mapper;
            _propertyImageRepository = propertyImageRepository;

        }

        public override async Task Delete(int id)
        {
            var image= await _propertyImageRepository.GetByIdAsync(id);

            if (image != null) 
            {
                UploadFileHelper.DeleteFile(image.ImageUrl);
                await _propertyImageRepository.DeleteAsync(image);
            }
        }





    }
}
