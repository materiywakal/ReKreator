using System;

namespace ReKreator.Domain.Enums
{
    [Flags]
    public enum NotificationPeriod
    {
        None = 0,
        Day = 1,
        ThreeDays = 2,
        Week = 4,
        TwoWeeks = 8,
        Month = 16
    }
}