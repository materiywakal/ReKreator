using System.Collections.Generic;
using ReKreator.Domain.Enums;

namespace ReKreator.Domain
{
    public class NotificationPeriodDictionary
    {
        public Dictionary<NotificationPeriod, int> Container { get; }

        public NotificationPeriodDictionary()
        {
            Container = new Dictionary<NotificationPeriod, int>
            {
                {NotificationPeriod.None, 0},
                {NotificationPeriod.Day, 24},
                {NotificationPeriod.ThreeDays, 72},
                {NotificationPeriod.Week, 168},
                {NotificationPeriod.TwoWeeks, 336},
                {NotificationPeriod.Month, 720}
            };
        }
    }
}