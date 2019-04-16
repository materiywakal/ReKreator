using AutoMapper;
using ReKreator.Domain;
using ReKreator.UI.MVC.Models.Account;

namespace ReKreator.UI.MVC.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AccountRegisterViewModel, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));

            CreateMap<AccountLogInViewModel, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Login))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Login))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<User, AccountProfileViewModel>()
                .ForMember(dest => dest.NoveltyMailingPeriod, opt => opt.MapFrom(src => src.UserMailing.MailingPeriod));

            CreateMap<User, AccountEditViewModel>()
                .ForMember(dest => dest.NoveltyMailingPeriod, opt => opt.MapFrom(src => src.UserMailing.MailingPeriod));
        }
    }
}
