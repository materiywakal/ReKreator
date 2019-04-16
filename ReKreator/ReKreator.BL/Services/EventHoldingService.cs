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
    public class EventHoldingService : IEventHoldingService
    {
        private readonly IUnitOfWork _db;
        private readonly IRepository<long, EventHolding> _repository;

        public EventHoldingService(IUnitOfWork uow, IRepository<long, EventHolding> repository)
        {
            _db = uow ?? throw new ArgumentNullException(nameof(uow));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<EventHolding>> GetAllAsync(Expression<Func<EventHolding, bool>> expression,
            Func<IQueryable<EventHolding>, IOrderedQueryable<EventHolding>> orderBy = null,
            int skip = 0,
            int? take = null,
            params Expression<Func<EventHolding, object>>[] includes)
        {
            return await _repository.GetAllAsync(expression, orderBy, skip, take, includes);
        }

        public async Task<EventHolding> GetAsync(Expression<Func<EventHolding, bool>> expression,
            params Expression<Func<EventHolding, object>>[] includes)
        {
            return await _repository.GetAsync(expression, includes);
        }

        public async Task UpdateNotificationTimeBeforeEventAsync(long id, NotificationPeriod periods, string username)
        {
            var entity = await _repository.GetAsync(eh => eh.EventHoldingId == id && eh.EventHoldings_Users.Any(ehu => ehu.User.UserName == username), eh => eh.EventHoldings_Users);
            entity.EventHoldings_Users.First().NotificationPeriodsBeforeEvent = periods;
            _repository.Update(entity);
            await _db.SaveAsync();
        }

        public async Task DeleteFavoriteAsync(string username, long eventHoldingId)
        {
            var item = await _db.EventHolding_UserRepository.GetAsync(ehu => ehu.User.UserName == username && ehu.EventHoldingId == eventHoldingId);
            _db.EventHolding_UserRepository.Delete(item);
            await _db.SaveAsync();
        }
    }
}
