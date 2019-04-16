using System;
using ReKreator.Domain.Enums;
using System.Collections.Generic;

namespace ReKreator.Domain
{
    public class Event
    {
        public long  EventId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public EventType Type { get; set; }
        public EventGenre Genres { get; set; }
        public string PosterUrl { get; set; }
        public string SourceUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual ICollection<EventHolding> EventsHoldings { get; set; }
    }
}
