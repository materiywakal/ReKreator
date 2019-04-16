using System.Collections.Generic;

namespace ReKreator.UI.MVC.Models.Event
{
    public class EventsViewModel
    {
        public ICollection<EventSearchViewModel> Events { get; set; }
        public EventFilterViewModel EventFilter { get; set; }
    }
}
