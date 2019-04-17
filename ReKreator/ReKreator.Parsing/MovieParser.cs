using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using ReKreator.Domain;
using ReKreator.Domain.Enums;
using ReKreator.Parsing.GenreDictionaries;
using ReKreator.Parsing.Interfaces;

namespace ReKreator.Parsing
{
    public class MovieParser : IParser
    {
        private readonly IBrowsingContext _context;
        private readonly int _days;

        private readonly MovieGenres _genres = new MovieGenres();

        private readonly ICollection<Event> _films = new List<Event>();
        private readonly ICollection<EventPlace> _filmsPlaces = new List<EventPlace>();
        private readonly ICollection<EventHolding> _filmsHoldings = new List<EventHolding>();

        // selectors for page with all movies
        private const string _moviesBlockSelector = "div.events-block > ul.b-lists.list_afisha.col-5 > li.lists__li";
        private const string _movieSourceUrlSelector = "a.media";

        //selectors for single movie page
        private const string _moviePosterUrlSelector = "td.image_wrapper > img";
        private const string _movieTitleSelector = "td.post.b-event-post > h1";
        private const string _movieSubTitleSelector = "td.post.b-event-post > div.sub_title";
        private const string _movieDescriptionSelector = "#event-description";
        private const string _movieGenreSelector = "td.genre > p > a";
        private const string _movieStartDateSelector = "div.outer_film-info > div > div.name > a";
        private const string _movieExpireDateSelector = "div.outer_film-info div.b-film-info:last-child > div.name > a";

        //selectors for eventsHoldings at single movie page
        private const string _movieHoldingSelector = "div.b-film-info";
        private const string _movieHoldingDateSelector = "div.name > a";
        private const string _movieHoldingPlacesSelector = "div > ul > li.b-film-list__li";

        //selectors for eventHoldingPlaces
        private const string _movieHoldingPlaceSelector = "div.film-name > a";
        private const string _movieHoldingTimesSelector = "div > div > div > ul.b-list.b-shedule-list > li";

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieParser"/> class.
        /// </summary>
        /// <param name="days">Number of days from the current date.</param>
        public MovieParser(int days)
        {
            var config = Configuration.Default.WithDefaultLoader();
            _context = BrowsingContext.New(config);
            _days = days;
        }

        public async Task<ParsingModel> ParseAsync()
        {
            var address = "https://afisha.tut.by/day/film/" + DateTime.Today.ToString(ParserUtilities.DateTimeFormat) +
                          "/" + DateTime.Today.AddDays(_days).ToString(ParserUtilities.DateTimeFormat) + "/";

            var document = await _context.OpenAsync(address);
            var elements = document.QuerySelectorAll(_moviesBlockSelector);
            foreach (var element in elements)
            {
                var sourceUrl = element.QuerySelector(_movieSourceUrlSelector).GetAttribute("href");
                await Task.Delay(TimeSpan.FromSeconds(ParserUtilities.QueryDelayTimeInSeconds));
                var movie = ParseMovie(await _context.OpenAsync(sourceUrl));
                movie.SourceUrl = sourceUrl;
                _films.Add(movie);
                Console.WriteLine("Movie: '" + movie.Title + "' has been parsed.");
            }

            return new ParsingModel {Events = _films, EventPlaces = _filmsPlaces, EventHoldings = _filmsHoldings};
        }

        private Event ParseMovie(IParentNode document)
        {
            var title = document.QuerySelector(_movieTitleSelector).TextContent;
            var description = document.QuerySelector(_movieDescriptionSelector).TextContent
                .Split("\nПоделиться:\n")[0]
                .TrimStart(' ', '\n', '\t').TrimEnd(' ', '\n', '\t');
            var genres = document.QuerySelectorAll(_movieGenreSelector)
                .Select(g => GetEnumEventGenre(g.Text()))
                .Aggregate(EventGenre.None, (current, genre) => current | genre);
            var poster = document.QuerySelector(_moviePosterUrlSelector)?.GetAttribute("src");
            var startDate = ParseMovieDate(document.QuerySelector(_movieStartDateSelector).TextContent);
            var expiryDate = ParseMovieDate(document.QuerySelector(_movieExpireDateSelector).TextContent);
            var currentMovie = new Event
            {
                Title = title,
                Description = description,
                PosterUrl = poster,
                Type = EventType.Movie,
                Genres = genres,
                StartDate = startDate,
                ExpiryDate = expiryDate,
                IsRemoved = false,
                EventsHoldings = new List<EventHolding>()
            };
            ParseEventHoldings(document, currentMovie);
            return currentMovie;
        }

        private void ParseEventHoldings(IParentNode document, Event currentMovie)
        {
            var movieDays = document.QuerySelectorAll(_movieHoldingSelector);
            foreach (var movieDay in movieDays)
            {
                if (movieDay.QuerySelector(_movieHoldingDateSelector) == null)
                    continue;
                var dayDate = ParseMovieDate(movieDay.QuerySelector(_movieHoldingDateSelector).TextContent);
                var moviePlaces = movieDay.QuerySelectorAll(_movieHoldingPlacesSelector);

                foreach (var moviePlace in moviePlaces)
                {
                    var placeTitle = moviePlace.QuerySelector(_movieHoldingPlaceSelector).TextContent
                        .TrimStart(' ', '\n', '\t').TrimEnd(' ', '\n', '\t');
                    var eventPlaceInCollection = _filmsPlaces.FirstOrDefault(e => e.Title == placeTitle);
                    EventPlace currentEventPlace;
                    if (eventPlaceInCollection == null)
                    {
                        var place = new EventPlace {Title = placeTitle, EventsHoldings = new List<EventHolding>()};
                        _filmsPlaces.Add(place);
                        currentEventPlace = place;
                    }
                    else
                    {
                        currentEventPlace = eventPlaceInCollection;
                    }

                    var movieTimes = moviePlace.QuerySelectorAll(_movieHoldingTimesSelector);

                    foreach (var movieTime in movieTimes)
                    {
                        var time = new DateTime(dayDate.Year, dayDate.Month, dayDate.Day,
                            int.Parse(movieTime.GetAttribute("data-hour")),
                            int.Parse(movieTime.GetAttribute("data-minute")), 0);

                        var eventHolding = new EventHolding
                        {
                            Time = time,
                            EventPlace = currentEventPlace,
                            Event = currentMovie
                        };
                        currentEventPlace.EventsHoldings.Add(eventHolding);
                        currentMovie.EventsHoldings.Add(eventHolding);
                        _filmsHoldings.Add(eventHolding);
                    }
                }
            }
        }

        private EventGenre GetEnumEventGenre(string stringGenre)
        {
            return _genres.Container.TryGetValue(stringGenre, out var genre) ? genre : EventGenre.None;
        }

        private DateTime ParseMovieDate(string date)
        {
            var result = DateTime.Parse(date.Split(',')[0]);
            if (result.Month < DateTime.Today.Month)
            {
                result = new DateTime(DateTime.Today.Year + 1, result.Month, result.Day);
            }

            return result;
        }
    }
}