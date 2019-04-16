using System;
using ReKreator.Domain.Enums;

namespace ReKreator.UI.MVC.Models.EventHolding
{
    public class EventHoldingViewModel
    {
        public long EventHoldingId { get; set; }

        public DateTime Time { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public EventType Type { get; set; }
        public EventGenre Genres { get; set; }
        public string PosterUrl { get; set; }
    }
}
