using System;
using System.Collections.Generic;
using ReKreator.Domain.Enums;

namespace ReKreator.UI.MVC.Models.Event
{
    public class EventViewModel
    {
        public long Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public EventType Type { get; set; }
        public EventGenre Genre { get; set; }
        public string PosterUrl { get; set; }
        public string SourceUrl { get; set; }
        public string ShortUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public ICollection<Domain.EventHolding> EventsHoldings { get; set; }
    }
}
