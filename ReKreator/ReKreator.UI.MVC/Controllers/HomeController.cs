using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReKreator.BL.Interfaces;
using ReKreator.Domain;
using ReKreator.Domain.Enums;
using ReKreator.Emailing;
using ReKreator.UI.MVC.Models.Contact;
using ReKreator.UI.MVC.Models.Event;

namespace ReKreator.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISender _sender;
        private readonly UserManager<User> _userManager;
        private readonly IEventService _eventService;
        private readonly IEventHoldingService _eventHoldingService;
        private readonly IMapper _mapper;

        public HomeController(ISender sender, UserManager<User> userManager, IEventService eventService,
            IEventHoldingService eventHoldingService, IMapper mapper)
        {
            _sender = sender;
            _userManager = userManager;
            _eventService = eventService;
            _eventHoldingService = eventHoldingService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var latestEvents =
                await _eventService.GetAllAsync(o => !o.IsRemoved, o => o.OrderBy(b => b.CreationDate), 0, 5);
            foreach (var item in latestEvents)
            {
                item.EventsHoldings = (ICollection<EventHolding>) await _eventHoldingService.GetAllAsync(
                    o => o.Event.SourceUrl == item.SourceUrl, null, 0, null, o => o.Event, o => o.EventPlace);
            }

            List<EventViewModel> viewModels = new List<EventViewModel>();
            foreach (var item in latestEvents)
            {
                EventViewModel temp = _mapper.Map<EventViewModel>(item);
                temp.ShortUrl = temp.SourceUrl.Split("/").SkipLast(1).Last();
                viewModels.Add(temp);
            }

            var nearestMovies = await _eventHoldingService.GetAllAsync(
                o => o.Event.Type == EventType.Movie && o.Time > DateTime.Now, o => o.OrderBy(d => d.Time), includes:
                o => o.Event);
            nearestMovies = nearestMovies.Distinct().Take(3);
            ViewData["Movies"] = new List<EventViewModel>();
            foreach (var item in nearestMovies)
            {
                EventViewModel temp = _mapper.Map<EventViewModel>(_mapper.Map<EventViewModel>(_eventService
                    .GetAllAsync(o => o.SourceUrl == item.Event.SourceUrl, null, 0, null, o => o.EventsHoldings).Result
                    .First()));
                temp.ShortUrl = temp.SourceUrl.Split("/").SkipLast(1).Last();
                ((List<EventViewModel>) ViewData["Movies"]).Add(temp);
            }

            var nearestConcerts = await _eventHoldingService.GetAllAsync(
                o => o.Event.Type == EventType.Concert && o.Time > DateTime.Now, o => o.OrderBy(d => d.Time),
                includes:
                o => o.Event);
            nearestConcerts = nearestConcerts.Distinct().Take(3);
            ViewData["Concerts"] = new List<EventViewModel>();
            foreach (var item in nearestConcerts)
            {
                EventViewModel temp = _mapper.Map<EventViewModel>(_mapper.Map<EventViewModel>(_eventService
                    .GetAllAsync(o => o.SourceUrl == item.Event.SourceUrl, null, 0, null, o => o.EventsHoldings).Result
                    .First()));
                temp.ShortUrl = temp.SourceUrl.Split("/").SkipLast(1).Last();
                ((List<EventViewModel>) ViewData["Concerts"]).Add(temp);
            }

            var nearestPerformances = await _eventHoldingService.GetAllAsync(
                o => o.Event.Type == EventType.Theatre && o.Time > DateTime.Now, o => o.OrderBy(d => d.Time), includes:
                o => o.Event);
            nearestPerformances = nearestPerformances.Distinct().Take(3);
            ViewData["Performances"] = new List<EventViewModel>();
            foreach (var item in nearestPerformances)
            {
                EventViewModel temp = _mapper.Map<EventViewModel>(_mapper.Map<EventViewModel>(_eventService
                    .GetAllAsync(o => o.SourceUrl == item.Event.SourceUrl, null, 0, null, o => o.EventsHoldings).Result
                    .First()));
                temp.ShortUrl = temp.SourceUrl.Split("/").SkipLast(1).Last();
                ((List<EventViewModel>) ViewData["Performances"]).Add(temp);
            }

            return View(viewModels);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string name = "Anonymous";
            string email = model.Email;
            if (User.Identity.IsAuthenticated)
            {
                name = User.Identity.Name;
                email = _userManager.FindByNameAsync(User.Identity.Name).Result.Email;
            }

            string text =
                $"{name} is contacting us! His email is {email}. The subject is {model.Subject}. {model.Text}";
            Task.Run(() => _sender.ContactMessageAsync(name, "ReKreator contact us.", text));
            return RedirectToAction("Index", "Home");
        }
    }
}