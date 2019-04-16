using System;
using ReKreator.UI.MVC.Models.Event.Enums;

namespace ReKreator.UI.MVC.Models.Event
{
    public class EventFilterViewModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public string Keyword { get; set; }
        public DateTime? BottomLineDate { get; set; }
        public DateTime? TopLineDate { get; set; }
        public EventGenreViewModel? Genres { get; set; }
        public int Type { get; set; }
        public bool IsShowObsoletes { get; set; }
    }
}
