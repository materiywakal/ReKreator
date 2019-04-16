using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ReKreator.BL.Interfaces;
using ReKreator.DAL.Interfaces;
using ReKreator.Domain;
using ReKreator.Domain.Enums;

namespace ReKreator.BL.Services
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<long, Event> _repository;

        public EventService(IUnitOfWork unitOfWork, IRepository<long, Event> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<IEnumerable<Event>> GetAllAsync(
            Expression<Func<Event, bool>> expression,
            Func<IQueryable<Event>, IOrderedQueryable<Event>> orderBy = null,
            int skip = 0, int? take = null,
            params Expression<Func<Event, object>>[] includes)
        {
            return await _repository.GetAllAsync(expression, orderBy, skip, take, includes);
        }

        public async Task<Event> GetEventByUrlAsync(string sourceUrl)
        {
            Event eventToShow =
                await _unitOfWork.EventRepository.GetAsync(o => o.SourceUrl.Contains("/" + sourceUrl + "/"));
            eventToShow.EventsHoldings =
                (ICollection<EventHolding>) await _unitOfWork.EventHoldingRepository.GetAllAsync(
                    o => o.Event.SourceUrl.Equals(eventToShow.SourceUrl, StringComparison.CurrentCulture) &&
                         o.Time > DateTime.Now, orderBy: o => o.OrderBy(t => t.Time),
                    includes: o => o.EventPlace);
            return eventToShow;
        }

        public async Task AddEventToFavoritesAsync(string username, int eventHoldingId)
        {
            EventHolding holding =
                await _unitOfWork.EventHoldingRepository.GetAsync(o => o.EventHoldingId == eventHoldingId);
            User user = await _unitOfWork.UserRepository.GetAsync(o =>
                o.UserName.Equals(username, StringComparison.CurrentCulture));
            EventHolding_User item = new EventHolding_User
            {
                EventHolding = holding, EventHoldingId = holding.EventHoldingId,
                NotificationPeriodsBeforeEvent = NotificationPeriod.None, User = user, UserId = user.Id
            };
            if (await _unitOfWork.EventHolding_UserRepository.GetAsync(o =>
                    o.User.UserName == item.User.UserName &&
                    o.EventHolding.EventHoldingId == item.EventHolding.EventHoldingId) == null)
            {
                _unitOfWork.EventHolding_UserRepository.Create(item);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task<IEnumerable<EventHolding_User>> GetUserEventHoldingsAsync(string username)
        {
            User user = await _unitOfWork.UserRepository.GetAsync(o =>
                o.UserName.Equals(username, StringComparison.CurrentCulture));
            return await _unitOfWork.EventHolding_UserRepository.GetAllAsync(o => o.User == user);
        }
    }
}