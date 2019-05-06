using System.ComponentModel.DataAnnotations;

namespace ReKreator.UI.MVC.Models.Event.Enums
{
    public enum TheatreGenresViewModel : ulong
    {
        [Display(Name = "-")] None = 0,

        [Display(Name = "Кукольное")] TheatrePuppet = 0x10000000000,
        [Display(Name = "Мелодрама")] TheatreMelodrama = 0x20000000000,
        [Display(Name = "Музыка")] TheatreMusic = 0x40000000000,
        [Display(Name = "Водевиль")] TheatreVaudeville = 0x80000000000,
        [Display(Name = "Музыкальное")] TheatreMusical = 0x100000000000,
        [Display(Name = "Опера")] TheatreOpera = 0x200000000000,
        [Display(Name = "Баллет")] TheatreBallet = 0x400000000000,
        [Display(Name = "Драма")] TheatreDrama = 0x800000000000,
        [Display(Name = "Комедия")] TheatreComedy = 0x1000000000000,
        [Display(Name = "Детское")] TheatreChild = 0x2000000000000,
        [Display(Name = "Моноспектакль")] TheatreSoloPerformance = 0x4000000000000,
        [Display(Name = "Оперетта")] TheatreOperetta = 0x8000000000000,
        [Display(Name = "Трагедия")] TheatreTragedy = 0x10000000000000,
        [Display(Name = "Трагикомедия")] TheatreTragicomedy = 0x20000000000000
    }
}