using System.Collections.Generic;

namespace ReKreator.Domain
{
    public class ParsingModel
    {
        public ICollection<Event> Events { get; set; }
        public ICollection<EventPlace> EventPlaces { get; set; }
        public ICollection<EventHolding> EventHoldings { get; set; }
    }
}
