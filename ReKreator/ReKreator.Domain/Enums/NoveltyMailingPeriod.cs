using System.ComponentModel.DataAnnotations;

namespace ReKreator.Domain.Enums
{
    public enum NoveltyMailingPeriod
    {
        None = 0,
        [Display(Name = "День")] Day = 24,
        [Display(Name = "Неделя")] Week = 168,
        [Display(Name = "Месяц")] Month = 720
    }
}