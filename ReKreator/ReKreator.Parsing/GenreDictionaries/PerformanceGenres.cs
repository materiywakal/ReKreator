using System.Collections.Generic;
using ReKreator.Domain.Enums;
using ReKreator.Parsing.Interfaces;

namespace ReKreator.Parsing.GenreDictionaries
{
    public class PerformanceGenres : IGenre
    {
        public EventType EventType { get; }
        public Dictionary<string, EventGenre> Container { get; }

        public PerformanceGenres()
        {
            EventType = EventType.Theatre;
            Container = new Dictionary<string, EventGenre>
            {
                {"Кукольный", EventGenre.TheatrePuppet},
                {"Мелодрама", EventGenre.TheatreMelodrama},
                {"Музыкальный", EventGenre.TheatreMusic},
                {"Водевиль", EventGenre.TheatreVaudeville},
                {"Мюзикл", EventGenre.TheatreMusical},
                {"Опера", EventGenre.TheatreOpera},
                {"Балет", EventGenre.TheatreBallet},
                {"Драма", EventGenre.TheatreDrama},
                {"Комедия", EventGenre.TheatreComedy},
                {"Детский", EventGenre.TheatreChild},
                {"Моноспектакль", EventGenre.TheatreSoloPerformance},
                {"Оперетта", EventGenre.TheatreOperetta},
                {"Трагедия", EventGenre.TheatreTragedy},
                {"Трагикомедия", EventGenre.TheatreTragicomedy}
            };
        }
    }
}