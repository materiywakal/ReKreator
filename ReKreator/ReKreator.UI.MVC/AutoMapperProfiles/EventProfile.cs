using System.Linq;
using AutoMapper;
using ReKreator.Domain;
using ReKreator.UI.MVC.Models.Event;

namespace ReKreator.UI.MVC.AutoMapperProfiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventViewModel>()
                .ForMember(d => d.Genre, o => o.MapFrom(m => m.Genres));
            CreateMap<Event, EventSearchViewModel>();
        }
    }
}
