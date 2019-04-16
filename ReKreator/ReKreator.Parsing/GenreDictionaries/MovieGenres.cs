using System.Collections.Generic;
using ReKreator.Domain.Enums;
using ReKreator.Parsing.Interfaces;

namespace ReKreator.Parsing.GenreDictionaries
{
    public class MovieGenres : IGenre
    {
        public EventType EventType { get; }
        public Dictionary<string, EventGenre> Container { get; }

        public MovieGenres()
        {
            EventType = EventType.Movie;
            Container = new Dictionary<string, EventGenre>
            {
                {"Анимация", EventGenre.MovieAnimation},
                {"Биография", EventGenre.MovieBiography},
                {"Боевик", EventGenre.MovieAction},
                {"Военный", EventGenre.MovieMilitary},
                {"Детский/Семейный", EventGenre.MovieFamily},
                {"Детектив", EventGenre.MovieDetective},
                {"Документальный", EventGenre.MovieDocumentary},
                {"Драма", EventGenre.MovieDrama},
                {"Исторический", EventGenre.MovieHistorical},
                {"Комедия", EventGenre.MovieComedy},
                {"Криминальный", EventGenre.MovieCriminal},
                {"Мелодрама", EventGenre.MovieMelodrama},
                {"Мюзикл", EventGenre.MovieMusical},
                {"Приключения", EventGenre.MovieAdventure},
                {"Триллер", EventGenre.MovieThriller},
                {"Ужасы", EventGenre.MovieHorror},
                {"Фантастика", EventGenre.MovieFantastic},
                {"Фэнтези", EventGenre.MovieFantasy},
                {"Спорт", EventGenre.MovieSport}
            };
        }
    }
}
