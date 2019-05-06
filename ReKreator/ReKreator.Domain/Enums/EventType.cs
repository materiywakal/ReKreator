using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ReKreator.Domain.Enums
{
    public enum EventType
    {
        [Display(Name = "-")] [Description("-")]
        None = 0,

        [Display(Name = "Кино")] [Description("Кино")]
        Movie,

        [Display(Name = "Концерт")] [Description("Концерт")]
        Concert,

        [Display(Name = "Спектакль")] [Description("Спектакль")]
        Theatre
    }
}