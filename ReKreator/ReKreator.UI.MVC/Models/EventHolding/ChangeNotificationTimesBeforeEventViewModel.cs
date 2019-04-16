using System.Collections.Generic;

namespace ReKreator.UI.MVC.Models.EventHolding
{
    public class ChangeNotificationTimesBeforeEventViewModel
    {
        public long EventHoldingId { get; set; }
        public ICollection<NotificationPeriodViewModel> NotificationPeriods { get; set; }
    }
}
