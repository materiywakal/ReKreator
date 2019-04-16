using System.Linq;
using AutoMapper;
using ReKreator.Domain;
using ReKreator.UI.MVC.Models.EventHolding;

namespace ReKreator.UI.MVC.AutoMapperProfiles
{
    public class EventHoldingProfile : Profile
    {
        public EventHoldingProfile()
        {
            CreateMap<EventHolding, EventHoldingViewModel>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Event.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Event.Description))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Event.Genres))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Event.Type))
                .ForMember(dest => dest.PosterUrl, opt => opt.MapFrom(src => src.Event.PosterUrl));

            CreateMap<EventHolding, EventHoldingDetailsViewModel>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Event.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Event.Description))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Event.Genres))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Event.Type))
                .ForMember(dest => dest.PosterUrl, opt => opt.MapFrom(src => src.Event.PosterUrl))
                .ForMember(dest => dest.SourceUrl, opt => opt.MapFrom(src => src.Event.SourceUrl))
                .ForMember(dest => dest.Place, opt => opt.MapFrom(src => src.EventPlace.Title))
                .ForMember(dest => dest.NotificationsPeriod, opt => opt.MapFrom(src => src.EventHoldings_Users.First().NotificationPeriodsBeforeEvent));

        }
    }
}
