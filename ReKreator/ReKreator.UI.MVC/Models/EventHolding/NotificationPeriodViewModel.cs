using System.ComponentModel;

namespace ReKreator.UI.MVC.Models.EventHolding
{
    public enum NotificationPeriodViewModel
    {
        [Description("-")] None = 0,
        [Description("День")] Day = 1,
        [Description("Три дня")] ThreeDays = 2,
        [Description("Неделя")] Week = 4,
        [Description("Две недели")] TwoWeeks = 8,
        [Description("Месяц")] Month = 16
    }
}