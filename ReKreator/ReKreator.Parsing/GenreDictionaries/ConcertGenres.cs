using System.Collections.Generic;
using ReKreator.Domain.Enums;
using ReKreator.Parsing.Interfaces;

namespace ReKreator.Parsing.GenreDictionaries
{
    public class ConcertGenres: IGenre
    {
        public EventType EventType { get; }
        public Dictionary<string, EventGenre> Container { get; }

        public ConcertGenres()
        {
            EventType = EventType.Concert;
            Container = new Dictionary<string, EventGenre>
            {
                {"Альтернатива", EventGenre.ConcertAlternative},
                {"Джаз/Блюз", EventGenre.ConcertJazzAndBlues},
                {"Классика", EventGenre.ConcertClassic},
                {"Поп", EventGenre.ConcertPop},
                {"Фестиваль", EventGenre.ConcertFestival},
                {"Хип-хоп и рэп", EventGenre.ConcertHipHopRap},
                {"Шансон", EventGenre.ConcertChanson},
                {"Юмор", EventGenre.ConcertHumor},
                {"Рок", EventGenre.ConcertRock},
                {"Метал", EventGenre.ConcertMetal}
            };
        }
    }
}