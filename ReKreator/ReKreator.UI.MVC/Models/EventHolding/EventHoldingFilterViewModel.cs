using System;
using ReKreator.Domain.Enums;
using ReKreator.UI.MVC.Models.Event.Enums;

namespace ReKreator.UI.MVC.Models.EventHolding
{
    public class EventHoldingFilterViewModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public string Keyword { get; set; }
        public DateTime? BottomLineDate { get; set; }
        public DateTime? TopLineDate { get; set; }
        public EventType? Type { get; set; }
        public EventGenreViewModel? Genres { get; set; }
    }
}
