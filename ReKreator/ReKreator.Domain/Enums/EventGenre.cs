using System;
using System.ComponentModel;

namespace ReKreator.Domain.Enums
{
    [Flags]
    public enum EventGenre : ulong
    {
        [Description("")]
        None = 0,

        [Description("Анимация")] MovieAnimation = 0x00000001,
        [Description("Биография")] MovieBiography = 0x00000002,
        [Description("Экшн")] MovieAction = 0x00000004,
        [Description("Военное")] MovieMilitary = 0x00000008,
        [Description("Семейное")] MovieFamily = 0x00000010,
        [Description("Детектив")] MovieDetective = 0x00000020,
        [Description("Документальное")] MovieDocumentary = 0x00000040,
        [Description("Драма")] MovieDrama = 0x00000080,
        [Description("Историческое")] MovieHistorical = 0x00000100,
        [Description("Комедия")] MovieComedy = 0x00000200,
        [Description("Криминальное")] MovieCriminal = 0x00000400,
        [Description("Мелодрама")] MovieMelodrama = 0x00000800,
        [Description("Музыкальное")] MovieMusical = 0x00001000,
        [Description("Приключение")] MovieAdventure = 0x00002000,
        [Description("Триллер")] MovieThriller = 0x00004000,
        [Description("Ужасы")] MovieHorror = 0x00008000,
        [Description("Фантастика")] MovieFantastic = 0x00010000,
        [Description("Фэнтези")] MovieFantasy = 0x00020000,
        [Description("Спорт")] MovieSport = 0x00040000,

        [Description("Альтернатива")] ConcertAlternative = 0x001000000,
        [Description("Джаз и блюз")] ConcertJazzAndBlues = 0x002000000,
        [Description("Классика")] ConcertClassic = 0x004000000,
        [Description("Поп")] ConcertPop = 0x008000000,
        [Description("Фестиваль")] ConcertFestival = 0x010000000,
        [Description("Хип-хоп и рэп")] ConcertHipHopRap = 0x020000000,
        [Description("Шансон")] ConcertChanson = 0x040000000,
        [Description("Юмор")] ConcertHumor = 0x080000000,
        [Description("Рок")] ConcertRock = 0x100000000,
        [Description("Метал")] ConcertMetal = 0x200000000,

        [Description("Кукольное")] TheatrePuppet = 0x10000000000,
        [Description("Мелодрама")] TheatreMelodrama = 0x20000000000,
        [Description("Музыка")] TheatreMusic = 0x40000000000,
        [Description("Водевиль")] TheatreVaudeville = 0x80000000000,
        [Description("Музыкальное")] TheatreMusical = 0x100000000000,
        [Description("Опера")] TheatreOpera = 0x200000000000,
        [Description("Баллет")] TheatreBallet = 0x400000000000,
        [Description("Драма")] TheatreDrama = 0x800000000000,
        [Description("Комедия")] TheatreComedy = 0x1000000000000,
        [Description("Детское")] TheatreChild = 0x2000000000000,
        [Description("Моноспектакль")] TheatreSoloPerformance = 0x4000000000000,
        [Description("Оперетта")] TheatreOperetta = 0x8000000000000,
        [Description("Трагедия")] TheatreTragedy = 0x10000000000000,
        [Description("Трагикомедия")] TheatreTragicomedy = 0x20000000000000
    }
}