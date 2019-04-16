using System;
using System.Collections.Generic;

namespace ReKreator.Domain
{
    public class EventHolding
    {
        public long EventHoldingId { get; set; }

        public DateTime Time { get; set; }

        public virtual Event Event { get; set; }
        public virtual EventPlace EventPlace { get; set; }
        public virtual ICollection<EventHolding_User> EventHoldings_Users { get; set; }
    }
}
