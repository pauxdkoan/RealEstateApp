

using AutoMapper;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.ViewModels.User;
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





            #endregion
        }
    }
}
