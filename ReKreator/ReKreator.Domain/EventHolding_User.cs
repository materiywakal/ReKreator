using ReKreator.Domain.Enums;

namespace ReKreator.Domain
{
    public class EventHolding_User
    {
        public long EventHoldingId { get; set; }
        public virtual EventHolding EventHolding { get; set; }

        public long UserId { get; set; }
        public virtual User User { get; set; }

        public NotificationPeriod NotificationPeriodsBeforeEvent { get; set; }
    }
}
