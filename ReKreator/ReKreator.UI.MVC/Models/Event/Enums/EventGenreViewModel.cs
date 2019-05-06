using System.ComponentModel.DataAnnotations;

namespace ReKreator.UI.MVC.Models.Event.Enums
{
    public enum EventGenreViewModel : ulong
    {
        [Display(Name = "-")]None = 0,

        [Display(Name = "Анимация")] MovieAnimation = 0x00000001,
        [Display(Name = "Биография")] MovieBiography = 0x00000002,
        [Display(Name = "Экшн")] MovieAction = 0x00000004,
        [Display(Name = "Военное")] MovieMilitary = 0x00000008,
        [Display(Name = "Семейное")] MovieFamily = 0x00000010,
        [Display(Name = "Детектив")] MovieDetective = 0x00000020,
        [Display(Name = "Документальное")] MovieDocumentary = 0x00000040,
        [Display(Name = "Драма")] MovieDrama = 0x00000080,
        [Display(Name = "Историческое")] MovieHistorical = 0x00000100,
        [Display(Name = "Комедия")] MovieComedy = 0x00000200,
        [Display(Name = "Криминальное")] MovieCriminal = 0x00000400,
        [Display(Name = "Мелодрама")] MovieMelodrama = 0x00000800,
        [Display(Name = "Музыкальное")] MovieMusical = 0x00001000,
        [Display(Name = "Приключение")] MovieAdventure = 0x00002000,
        [Display(Name = "Триллер")] MovieThriller = 0x00004000,
        [Display(Name = "Ужасы")] MovieHorror = 0x00008000,
        [Display(Name = "Фантастика")] MovieFantastic = 0x00010000,
        [Display(Name = "Фэнтези")] MovieFantasy = 0x00020000,
        [Display(Name = "Спорт")] MovieSport = 0x00040000,

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