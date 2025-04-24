

using AutoMapper;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Dtos.Improvement;
using RealEstateApp.Core.Application.Dtos.PropertyType;
using RealEstateApp.Core.Application.Dtos.SaleType;
using RealEstateApp.Core.Application.Features.Account.Commands.RegisterDevelopAsync;
using RealEstateApp.Core.Application.Features.Account.Queries.AuthenticateAsync;
using RealEstateApp.Core.Application.Features.Improvement.Commands.CreateImprovement;
using RealEstateApp.Core.Application.Features.Improvement.Commands.UpdateImprovement;
using RealEstateApp.Core.Application.Features.PropertyType.Commands.CreatePropertyType;
using RealEstateApp.Core.Application.Features.PropertyType.Commands.UpdatePropertyType;
using RealEstateApp.Core.Application.Features.SaleType.Commands.CreateSaleType;
using RealEstateApp.Core.Application.Features.SaleType.Commands.UpdateSaleType;
using RealEstateApp.Core.Application.ViewModels.Agent;
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
using RealEstateApp.Core.Application.ViewModels.User.Client;
using RealEstateApp.Core.Application.ViewModels.User.Developer;
using RealEstateApp.Core.Domain.Entities;
using RealStateApp.Core.Application.ViewModels.Agent;

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
                .ForMember(x => x.Rol, opt => opt.Ignore())
                .ReverseMap();

            //SaveAdminVm-SaveUserVm
            CreateMap<SaveAdminVm, SaveUserVm>()
                .ForMember(x => x.Photo, opt => opt.Ignore())
                .ForMember(x => x.File, opt => opt.Ignore())
                .ForMember(x => x.Phone, opt => opt.Ignore())
                .ForMember(x => x.Rol, opt => opt.Ignore())
                .ReverseMap()
                ;


            //SaveAdminVm-SaveUserVm
            CreateMap<EditAdminVm, SaveUserVm>()
                .ForMember(x => x.Photo, opt => opt.Ignore())
                .ForMember(x => x.File, opt => opt.Ignore())
                .ForMember(x => x.Phone, opt => opt.Ignore())
                .ForMember(x => x.Rol, opt => opt.Ignore())
                .ReverseMap();


            //SaveAdminVm-SaveUserVm
            CreateMap<SaveUserVm, UpdateRequest>()
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.Photo))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.IdentityCard, opt => opt.MapFrom(src => src.IdentityCard))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ReverseMap();



            //SaveAdminVm-SaveUserVm
            CreateMap<SaveAdminVm, UserVm>()
                .ReverseMap();

            //SaveAdminVm-SaveUserVm
            CreateMap<EditAdminVm, UserVm>()
                .ReverseMap();

            //SaveDeveloperVm -> SaveUserVm
            CreateMap<SaveDeveloperVm, SaveUserVm>()
                .ForMember(x => x.Photo, opt => opt.Ignore())
                .ForMember(x => x.File, opt => opt.Ignore())
                .ForMember(x => x.Phone, opt => opt.Ignore())
                .ForMember(x => x.Rol, opt => opt.Ignore())
                .ReverseMap();

            //DeveloperVm -> UserVm
            CreateMap<DeveloperVm, UserVm>()
                .ForMember(x => x.Photo, opt => opt.Ignore())
                .ForMember(x => x.IsActive, opt => opt.Ignore())
                .ForMember(x => x.Phone, opt => opt.Ignore())
                .ReverseMap();

            //EditDeveloperVm -> UserVm
            CreateMap<EditDeveloperVm, UserVm>()
                .ForMember(x => x.Photo, opt => opt.Ignore())
                .ForMember(x => x.IsActive, opt => opt.Ignore())
                .ForMember(x => x.Phone, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.HasError, opt =>opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore());

            //EditDeveloperVm -> SaveUserVm
            CreateMap<EditDeveloperVm, SaveUserVm>()
                .ForMember(x => x.Photo, opt => opt.Ignore())
                .ForMember(x => x.IsActive, opt => opt.Ignore())
                .ForMember(x => x.Phone, opt => opt.Ignore())
                .ForMember(x => x.Rol, opt => opt.Ignore())
                .ReverseMap();

            //AgentViewModel -> UserVm
            CreateMap<UserVm, AgentViewModel>()
                .ForMember(x => x.FullName,
                       opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(x => x.PhotoUrl,
                       opt => opt.MapFrom(src => src.Photo))
                .ForMember(x => x.PhoneNumber,
                       opt => opt.MapFrom(src => src.Phone))
                .ForMember(x => x.properties,
                       opt => opt.Ignore())
                .ForMember(x => x.Id,
                       opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Email,
                       opt => opt.MapFrom(src => src.Email))
                .ForMember(x => x.IsActive,
                       opt => opt.MapFrom(src => src.IsActive));

            //EditAgentViewModel -> user
            CreateMap<User, EditAgentViewModel>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.FotoUrl, opt => opt.MapFrom(src => src.Photo))
                .ForMember(dest => dest.Foto, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Apellido))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Telefono))
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.FotoUrl))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.Ignore())
                .ForMember(dest => dest.Rol, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                .ForMember(dest => dest.IdentityCard, opt => opt.Ignore())
                .ForMember(dest => dest.Properties, opt => opt.Ignore())
                .ForMember(dest => dest.Offers, opt => opt.Ignore())
                .ForMember(dest => dest.FavoriteProperties, opt => opt.Ignore())
                .ForMember(dest => dest.ClientChats, opt => opt.Ignore())
                .ForMember(dest => dest.AgentChats, opt => opt.Ignore())
                .ForMember(dest => dest.Messages, opt => opt.Ignore());

                // Mapeo User -> ClientVm
            CreateMap<User, ClientVm>()
                .ForMember(dest => dest.CurrentProperties, opt => opt.Ignore())
                .ForMember(dest => dest.FavoriteProperties, opt => opt.MapFrom(src => src.FavoriteProperties));

            ;
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

            //PropertyType->SavePropertyTypeVm
            CreateMap<PropertyType, SavePropertyTypeVm>()
                .ReverseMap()
                .ForMember(x => x.Properties, opt => opt.Ignore());
            #endregion

            #region FavoritePropertyProfile

            //FavoriteProperty->FavoritePropertyVm
            // Mapeo FavoriteProperty -> FavoritePropertyVm
            CreateMap<FavoriteProperty, FavoritePropertyVm>()
                .ForMember(dest => dest.Client, opt => opt.Ignore()); // Evitamos ciclo aquí


            #endregion

            #region SalesTypeProfile
            //SalesType->SalesTypeVm
            CreateMap<SalesType, SalesTypeVm>()
                .ReverseMap();

            //SalesType->SaveSalesTypeVm
            CreateMap<SalesType, SaveSalesTypeVm>()
                .ReverseMap()
                .ForMember(x => x.Properties, opt => opt.Ignore());
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

            //Improvement->SaveImprovementVm
            CreateMap<Improvement, SaveImprovementVm>()
                .ReverseMap()
                .ForMember(x => x.PropertyImprovements, opt => opt.Ignore());
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

            //Chat ->SaveChatVm
            CreateMap<Chat, SaveChatViewModel>()
                .ReverseMap()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Property, opt => opt.Ignore())
                .ForMember(x => x.Client, opt => opt.Ignore())
                .ForMember(x => x.Agent, opt => opt.Ignore())
                .ForMember(x => x.Messages, opt => opt.Ignore());
            #endregion

            #region MessageProfile
            //Message->MessageVm
            CreateMap<Message, MessageVm>()
                .ReverseMap();

            //Message->SaveMessageVm
            CreateMap<Message, SaveMessageVm>()
                .ReverseMap()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Chat, opt => opt.Ignore())
                .ForMember(x => x.Sender, opt => opt.Ignore());
            #endregion


            #region SQRS
            CreateMap<CreatePropertyTypeCommand, PropertyType>()
                .ReverseMap();

            CreateMap<UpdatePropertyTypeCommand, PropertyType>()
                .ReverseMap();

            CreateMap<PropertyTypeUpdateResponse, PropertyType>()
              .ReverseMap();

            CreateMap<PropertyTypeDto, PropertyType>()
               .ReverseMap();

            CreateMap<CreateSaleTypeCommand, SalesType>()
            .ReverseMap();

            CreateMap<UpdateSaleTypeCommand, SalesType>()
                .ReverseMap();

            CreateMap<SaleTypeUpdateResponse, SalesType>()
                .ReverseMap();

            CreateMap<SalesTypeDto, SalesType>()
               .ReverseMap();


            CreateMap<UpdateImprovementCommand, Improvement>()
               .ReverseMap();


            CreateMap<ImprovementUpdateResponse, Improvement>()
               .ReverseMap();

            CreateMap<CreateImprovementCommand, Improvement>()
            .ReverseMap();


            CreateMap<ImprovementDto, Improvement>()
            .ReverseMap();


            CreateMap<AuthenticateAsyncQuery, AuthenticationRequest>()
           .ReverseMap();


            CreateMap<RegisterDeveloperUserAsyncCommand, RegisterRequest>()
           .ReverseMap();

            







            #endregion

        }
    }
}
