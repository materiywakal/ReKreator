using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using ReKreator.Domain;
using ReKreator.Domain.Enums;
using ReKreator.Parsing.Interfaces;

namespace ReKreator.Parsing
{
    public class GenericParser<TGenre> : IParser
        where TGenre : IGenre, new()

    {
        private readonly IBrowsingContext _context;
        private readonly int _days;

        private readonly TGenre _genres = new TGenre();

        private readonly ICollection<Event> _events = new List<Event>();
        private readonly ICollection<EventPlace> _places = new List<EventPlace>();
        private readonly ICollection<EventHolding> _holdings = new List<EventHolding>();

        private readonly string _mainReference;

        private const string _blockSelector = "#schedule-table > div.b-film-info";

        private const string _sourceUrlSelector =
            "div.film-list-wrapper > ul.b-film-list > li.b-film-list__li > div.film-name > a";

        private const string _posterUrlSelector = "img.main_image";
        private const string _titleSelector = "#event-name";
        private const string _descriptionSelector = "#event-description";
        private const string _genreSelector = "div.sub_title > a";
        private const string _startDateSelector = "div.b-event__tickets > div.b-shedule-day > time";
        private const string _expireDateSelector = "div.b-event__tickets > div.b-shedule-day";
        private const string _holdingSelector = "div.b-event__tickets > div.b-shedule-day";
        private const string _holdingPlaceSelector = "a.b-event_place";
        private const string _holdingPlacesSelector = "td.b-shedule__place > a";

        /// <summary>
        /// Initializes a new instance of the <see cref="PerformanceParser{TGenre}"/> class.
        /// </summary>
        /// <param name="days">Number of days from the current date.</param>
        /// <param name="reference">Example: https://afisha.tut.by/day/theatre/ </param>
        public GenericParser(int days, string reference)
        {
            var config = Configuration.Default.WithDefaultLoader();
            _context = BrowsingContext.New(config);
            _days = days;
            _mainReference = reference;
        }

        public async Task<ParsingModel> ParseAsync()
        {
            var address = _mainReference +
                          DateTime.Today.ToString(ParserUtilities.DateTimeFormat) + "/" +
                          DateTime.Today.AddDays(_days).ToString(ParserUtilities.DateTimeFormat) + "/";

            var document = await _context.OpenAsync(address);
            var elements = document.QuerySelectorAll(_blockSelector);
            foreach (var element in elements)
            {
                if (element.QuerySelector(_sourceUrlSelector) == null)
                {
                    continue;
                }

                var sourceUrl = element.QuerySelector(_sourceUrlSelector).GetAttribute("href");
                await Task.Delay(TimeSpan.FromSeconds(ParserUtilities.QueryDelayTimeInSeconds));
                var _event = ParseEvent(await _context.OpenAsync(sourceUrl));
                _event.SourceUrl = sourceUrl;
                _events.Add(_event);
                Console.WriteLine("Event: " + _event.Title + " has been parsed.");
            }

            return new ParsingModel
                {Events = _events, EventPlaces = _places, EventHoldings = _holdings};
        }

        private Event ParseEvent(IParentNode document)
        {
            var title = document.QuerySelector(_titleSelector).TextContent;
            var description = document.QuerySelector(_descriptionSelector).TextContent
                .Split("\nПоделиться:\n")[0]
                .TrimStart(' ', '\n', '\t').TrimEnd(' ', '\n', '\t');
            var genres = document.QuerySelectorAll(_genreSelector)
                .Select(g => GetEnumEventGenre(g.Text()))
                .Aggregate(EventGenre.None, (current, genre) => current | genre);
            var poster = document.QuerySelector(_posterUrlSelector)?.GetAttribute("src");
            var startDate = DateTime.Parse(document.QuerySelector(_startDateSelector).GetAttribute("datetime"));
            var expiryDate = DateTime.Parse(document.QuerySelectorAll(_expireDateSelector).Last()
                .QuerySelector("time").GetAttribute("datetime"));
            var currentEvent = new Event
            {
                Title = title,
                Description = description,
                PosterUrl = poster,
                Type = _genres.EventType,
                Genres = genres,
                StartDate = startDate,
                ExpiryDate = expiryDate,
                IsRemoved = false,
                EventsHoldings = new List<EventHolding>()
            };
            ParseEventHoldings(document, currentEvent);
            return currentEvent;
        }

        private void ParseEventHoldings(IParentNode document, Event currentEvents)
        {
            var days = document.QuerySelectorAll(_holdingSelector);
            foreach (var day in days)
            {
                var dayDate = DateTime.Parse(day.QuerySelector("time").GetAttribute("datetime"));
                IElement place = document.QuerySelector(_holdingPlaceSelector);
                string placeTitle;
                if (place == null)
                {
                    placeTitle = day.QuerySelector(_holdingPlacesSelector).TextContent;
                }
                else
                {
                    placeTitle = place.QuerySelector("span").TextContent.TrimStart(' ', '\n', '\t')
                        .TrimEnd(' ', '\n', '\t');
                }

                var eventPlaceInCollection = _places.FirstOrDefault(e => e.Title == placeTitle);
                EventPlace currentEventPlace;
                if (eventPlaceInCollection == null)
                {
                    var _place = new EventPlace {Title = placeTitle, EventsHoldings = new List<EventHolding>()};
                    _places.Add(_place);
                    currentEventPlace = _place;
                }
                else
                {
                    currentEventPlace = eventPlaceInCollection;
                }

                var eventHolding = new EventHolding
                {
                    Time = dayDate,
                    EventPlace = currentEventPlace,
                    Event = currentEvents
                };
                currentEventPlace.EventsHoldings.Add(eventHolding);
                currentEvents.EventsHoldings.Add(eventHolding);
                _holdings.Add(eventHolding);
            }
        }

        private EventGenre GetEnumEventGenre(string stringGenre)
        {
            return _genres.Container.TryGetValue(stringGenre, out var genre) ? genre : EventGenre.None;
        }
    }
}