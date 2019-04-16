using System.Collections.Generic;

namespace ReKreator.Domain
{
    public class EventPlace
    {
        public long EventPlaceId { get; set; }

        public string Title { get; set; }

        public virtual ICollection<EventHolding> EventsHoldings { get; set; }
    }
}
