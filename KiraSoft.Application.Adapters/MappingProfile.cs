using AutoMapper;
using KiraSoft.Application.IdentityViewModel;
using KiraSoft.Domain.Model.Identity;

namespace KiraSoft.Application.Adapters
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
                //.ForMember(m => m.PasswordHash, o => o.Ignore())
                //.ForMember(m => m.SecurityStamp, o => o.Ignore());
            CreateMap<UserLogin, UserLoginViewModel>().ReverseMap();
            CreateMap<UserClaim, UserClaimViewModel>().ReverseMap();
            CreateMap<UserViewModel, User>();
            CreateMap<UserRole, UserRoleViewModel>().ReverseMap();
            CreateMap<Role, RoleViewModel>().ReverseMap();
        }
    }
}