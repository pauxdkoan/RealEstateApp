

using AutoMapper;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.ViewModels.Chat;
using RealEstateApp.Core.Application.ViewModels.Improvement;
using RealEstateApp.Core.Application.ViewModels.Improvement.PropertyImprovement;
using RealEstateApp.Core.Application.ViewModels.Offer;
using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.Property.FavoriteProperty;
using RealEstateApp.Core.Application.ViewModels.Property.PropertyImage;
using RealEstateApp.Core.Application.ViewModels.Property.PropertyType;
using RealEstateApp.Core.Application.ViewModels.SalesType;
using RealEstateApp.Core.Application.ViewModels.User;
using RealEstateApp.Core.Application.ViewModels.User.Admin;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Mappings
{
    public class GeneralProfile:Profile
    {
        public GeneralProfile() 
        {
            #region UserProfile

            // AuthenticationRequest -> LoginVm
            CreateMap<AuthenticationRequest, LoginVm>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            //RegisterRequest -> SaveUserVm
            CreateMap<RegisterRequest, SaveUserVm>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();


            //User->UserVm
            CreateMap<User, UserVm>()
                .ReverseMap();

            //User->SaveUserVm
            CreateMap<User, SaveUserVm>()
                .ForMember(x=>x.Error, opt => opt.Ignore())
                .ForMember(x=>x.HasError, opt => opt.Ignore())
                .ForMember(x => x.RolList, opt => opt.Ignore())
                .ForMember(x => x.File, opt => opt.Ignore())
                .ForMember(x => x.ConfirmPassword, opt => opt.Ignore())
                .ReverseMap();

            //SaveAdminVm-SaveUserVm
            CreateMap<SaveAdminVm, SaveUserVm>()
                .ForMember(x=>x.Photo, opt => opt.Ignore())
                .ForMember(x => x.File, opt => opt.Ignore())
                .ForMember(x => x.Phone, opt => opt.Ignore())
                .ForMember(x => x.Rol, opt => opt.Ignore())



                .ReverseMap();






            #endregion

            #region PropertyProfile
            //Property->PropertyVm
            CreateMap<Property, PropertyVm>()
                .ReverseMap();

            //Property-SavePropertyVm
            CreateMap<Property, SavePropertyVm>()
            .ReverseMap();

            //PropertyVm-SavePropertyVm
            CreateMap<PropertyVm, SavePropertyVm>()
            .ReverseMap();

            //EditPropertyVm-SavePropertyVm
            CreateMap<EditPropertyVm, SavePropertyVm>()
                .ReverseMap();




            #endregion

            #region PropertyImageProfile

            //PropertyImage->PropertyImageVm
            CreateMap<PropertyImage, PropertyImageVm>()
                .ReverseMap();

            //PropertyImage->SavePropertyImageVm
            CreateMap<PropertyImage, SavePropertyImageVm>()
            .ReverseMap();



            #endregion

            #region PropertyTypeProfile

            //PropertyType->PropertyTypeVm
            CreateMap<PropertyType, PropertyTypeVm>()
                .ReverseMap();
            #endregion

            #region FavoritePropertyProfile

            //FavoriteProperty->FavoritePropertyVm
            CreateMap<FavoriteProperty, FavoritePropertyVm>()
                .ReverseMap();


            #endregion

            #region SalesTypeProfile
            //SalesType->SalesTypeVm
            CreateMap<SalesType, SalesTypeVm>()
                .ReverseMap();
            #endregion

            #region PropertyImprovementsProfile
            //PropertyImprovement->PropertyImprovementVm
            CreateMap<PropertyImprovement, PropertyImprovementVm>()
                .ReverseMap();
            #endregion

            #region ImprovementProfile
            //Improvement->ImprovementVm
            CreateMap<Improvement, ImprovementVm>()
                .ReverseMap();
            #endregion

            #region OffersProfile
            //Offer->OfferVm
            CreateMap<Offer, OfferVm>()
                .ReverseMap();
            #endregion

            #region ChatsProfile
            //Chat->ChatVm
            CreateMap<Chat, ChatVm>()
                .ReverseMap();
            #endregion

        }
    }
}
