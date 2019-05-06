using System.ComponentModel.DataAnnotations;

namespace ReKreator.UI.MVC.Models.Event.Enums
{
    public enum ConcertGenresViewModel : ulong
    {
        [Display(Name = "-")]None = 0,
        [Display(Name = "Альтернатива")] ConcertAlternative = 0x001000000,
        [Display(Name = "Джаз и блюз")] ConcertJazzAndBlues = 0x002000000,
        [Display(Name = "Классика")] ConcertClassic = 0x004000000,
        [Display(Name = "Поп")] ConcertPop = 0x008000000,
        [Display(Name = "Фестиваль")] ConcertFestival = 0x010000000,
        [Display(Name = "Хип-хоп и рэп")] ConcertHipHopRap = 0x020000000,
        [Display(Name = "Шансон")] ConcertChanson = 0x040000000,
        [Display(Name = "Юмор")] ConcertHumor = 0x080000000,
        [Display(Name = "Рок")] ConcertRock = 0x100000000,
        [Display(Name = "Метал")] ConcertMetal = 0x200000000,
    }
}
