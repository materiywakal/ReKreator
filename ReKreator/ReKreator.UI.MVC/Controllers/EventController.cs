using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReKreator.BL.Interfaces;
using ReKreator.Domain;
using ReKreator.Domain.Enums;
using ReKreator.UI.MVC.Constants;
using ReKreator.UI.MVC.Models.Event;
using ReKreator.UI.MVC.Models.EventHolding;

namespace ReKreator.UI.MVC.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventHoldingService _eventHoldingService;
        private readonly IMapper _mapper;
        private readonly IEventService _eventService;

        public EventController(IEventHoldingService eventHoldingService, IMapper mapper, IEventService service)
        {
            _eventHoldingService = eventHoldingService ?? throw new ArgumentException(nameof(eventHoldingService));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _eventService = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<IActionResult> Event(string sourceUrl)
        {
            var item = await _eventService.GetEventByUrlAsync(sourceUrl);
            if (item == null)
            {
                return RedirectToAction("Error404", "Error");
            }
            var viewItem = _mapper.Map<EventViewModel>(item);
            viewItem.ShortUrl = viewItem.SourceUrl.Split("/").SkipLast(1).Last();
            if (User.Identity.IsAuthenticated)
            {
                ViewData["Favorites"] = await _eventService.GetUserEventHoldingsAsync(User.Identity.Name);
            }

            return View(viewItem);
        }

        [HttpPost]
        public async Task<ActionResult> EventsPage([FromBody]EventFilterViewModel filter)
        {
            if ((EventType)filter.Type == EventType.None)
                return StatusCode((int)HttpStatusCode.NotFound);

            var events = await _eventService.GetAllAsync(e =>
                e.Type == (EventType)filter.Type &&
                (filter.IsShowObsoletes || e.ExpiryDate >= DateTime.Today) &&
                (filter.TopLineDate == null || e.StartDate <= filter.TopLineDate) &&
                (filter.BottomLineDate == null || e.ExpiryDate >= filter.BottomLineDate) &&
                (string.IsNullOrEmpty(filter.Keyword) || (e.Title.Contains(filter.Keyword) || e.Description.Contains(filter.Keyword))));
            if (filter.Genres.HasValue)
                events = events.Where(eh => eh.Genres.HasFlag((EventGenre)filter.Genres.Value)).ToList();

            ViewData["totalSize"] = events.Count();
            ViewData["type"] = filter.Type.ToString();
            events = events.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);

            var models = _mapper.Map<List<Event>, List<EventSearchViewModel>>(events.ToList());
            return PartialView("EventItems", models);
        }

        [Authorize]
        public async Task<StatusCodeResult> AddToFavorites(int eventHoldingId)
        {
            await _eventService.AddEventToFavoritesAsync(User.Identity.Name, eventHoldingId);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> Events(EventFilterViewModel filter, EventType type)
        {
            if (type == EventType.None)
                return StatusCode((int)HttpStatusCode.NotFound);

            filter.PageNumber = 1;
            filter.PageSize = PaginationConstants.EventsPageSize;
            var events = await _eventService.GetAllAsync(e => e.Type == type && e.ExpiryDate >= DateTime.Today);

            ViewData["totalSize"] = events.Count();
            ViewData["type"] = type.ToString();
            events = events.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);

            var eventsViewModel = new EventsViewModel
            {
                Events = _mapper.Map<List<Event>, List<EventSearchViewModel>>(events.ToList()),
                EventFilter = new EventFilterViewModel { PageSize = filter.PageSize, PageNumber = filter.PageNumber, Type = (int)type }
            };

            return View("Events", eventsViewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Favorite(long id)
        {
            var eventHolding = await _eventHoldingService.GetAsync(eh =>
                eh.EventHoldingId == id && eh.EventHoldings_Users.Any(ehu => ehu.User.UserName == User.Identity.Name), 
                eh => eh.Event, eh => eh.EventPlace, eh => eh.EventHoldings_Users);

            if (eventHolding == null)
                return RedirectToAction("Error404", "Error");

            var model = _mapper.Map<EventHoldingDetailsViewModel>(eventHolding);
            return View("Favorite", model);
        }

        [HttpGet]
        [Authorize]
        public async Task<RedirectToActionResult> Delete(long id)
        {
            var eventHolding = await _eventHoldingService.GetAsync(eh =>
                    eh.EventHoldingId == id && eh.EventHoldings_Users.Any(ehu => ehu.User.UserName == User.Identity.Name),
                eh => eh.Event, eh => eh.EventPlace, eh => eh.EventHoldings_Users);

            if (eventHolding == null)
                return RedirectToAction("Error404", "Error");

            await _eventHoldingService.DeleteFavoriteAsync(User.Identity.Name, id);
            return RedirectToAction("Favorites");
        }

        [HttpPost]
        [Authorize]
        public async Task<StatusCodeResult> ChangeNotificationBeforeEvent([FromBody]ChangeNotificationTimesBeforeEventViewModel model)
        {
            var eventHolding = await _eventHoldingService.GetAsync(eh =>
                    eh.EventHoldingId == model.EventHoldingId && eh.EventHoldings_Users.Any(ehu => ehu.User.UserName == User.Identity.Name));

            if (eventHolding == null)
                return StatusCode((int)HttpStatusCode.NotFound);

            await _eventHoldingService.UpdateNotificationTimeBeforeEventAsync(model.EventHoldingId,
                model.NotificationPeriods.Aggregate(NotificationPeriod.None, (current, period) => current | (NotificationPeriod)period), User.Identity.Name);
            return Ok();
        }

        [HttpPost]
        [Authorize]
        public async Task<PartialViewResult> FavoritesPage([FromBody]EventHoldingFilterViewModel filter)
        {
            var eventHoldings = await _eventHoldingService.GetAllAsync(eh => 
                    eh.EventHoldings_Users.Any(ehu => ehu.User.UserName == User.Identity.Name) &&
                    (filter.TopLineDate == null || eh.Time <= filter.TopLineDate) &&
                    (filter.BottomLineDate == null || eh.Time >= filter.BottomLineDate) &&
                    ((filter.Type == null || filter.Type == EventType.None) || eh.Event.Type.Equals(filter.Type.Value)) &&
                    (string.IsNullOrEmpty(filter.Keyword) || (eh.Event.Title.Contains(filter.Keyword) || eh.Event.Description.Contains(filter.Keyword) || eh.EventPlace.Title.Contains(filter.Keyword))),
                    includes: new Expression<Func<EventHolding, object>>[] { eh => eh.Event, eh => eh.EventPlace });

            if(filter.Genres.HasValue)
                eventHoldings = eventHoldings.Where(eh => eh.Event.Genres.HasFlag((EventGenre)filter.Genres.Value)).ToList();

            ViewData["totalSize"] = eventHoldings.Count();
            eventHoldings = eventHoldings.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);

            var models = _mapper.Map<List<EventHolding>, List<EventHoldingViewModel>>(eventHoldings.ToList());
            return PartialView("EventHoldingsItems", models);
        }

        [HttpGet]
        [Authorize]
        public async Task<ViewResult> Favorites(EventHoldingFilterViewModel filter)
        {
            filter.PageNumber = 1;
            filter.PageSize = PaginationConstants.FavoritesPageSize;
            var eventHoldings = await _eventHoldingService.GetAllAsync(
                eh => eh.EventHoldings_Users.Any(ehu => ehu.User.UserName == User.Identity.Name),
                includes: new Expression<Func<EventHolding, object>>[] { eh => eh.Event, eh => eh.EventPlace });

            ViewData["totalSize"] = eventHoldings.Count();
            eventHoldings = eventHoldings.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);

            var favoritesViewModel = new FavoritesViewModel
            {
                EventHoldings = _mapper.Map<List<EventHolding>, List<EventHoldingViewModel>>(eventHoldings.ToList()),
                EventHoldingFilter = new EventHoldingFilterViewModel { PageSize = filter.PageSize, PageNumber = filter.PageNumber }
            };

            return View("Favorites", favoritesViewModel);
        }

    }
}