using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ReKreator.Domain.Enums
{
    public enum NoveltyMailingPeriod
    {
        [Display(Name = "-")] [Description("-")]
        None = 0,

        [Display(Name = "День")] [Description("День")]
        Day = 24,

        [Display(Name = "Неделя")] [Description("Неделя")]
        Week = 168,

        [Display(Name = "Месяц")] [Description("Месяц")]
        Month = 720
    }
}