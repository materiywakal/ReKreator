using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ReKreator.Domain;
using ReKreator.Domain.Enums;

namespace ReKreator.BL.Interfaces
{
    public interface IEventHoldingService
    {
        Task<IEnumerable<EventHolding>> GetAllAsync(Expression<Func<EventHolding, bool>> expression,
            Func<IQueryable<EventHolding>, IOrderedQueryable<EventHolding>> orderBy = null,
            int skip = 0,
            int? take = null,
            params Expression<Func<EventHolding, object>>[] includes);

        Task<EventHolding> GetAsync(Expression<Func<EventHolding, bool>> expression,
            params Expression<Func<EventHolding, object>>[] includes);

        Task UpdateNotificationTimeBeforeEventAsync(long id, NotificationPeriod periods, string username);

        Task DeleteFavoriteAsync(string username, long eventHoldingId);
    }
}
