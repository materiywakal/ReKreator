using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ReKreator.Domain;

namespace ReKreator.BL.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllAsync(Expression<Func<Event, bool>> expression,
            Func<IQueryable<Event>, IOrderedQueryable<Event>> orderBy = null,
            int skip = 0,
            int? take = null,
            params Expression<Func<Event, object>>[] includes);

        Task<Event> GetEventByUrlAsync(string sourceUrl);
        Task AddEventToFavoritesAsync(string username, int eventHoldingId);
        Task<IEnumerable<EventHolding_User>> GetUserEventHoldingsAsync(string username);
    }
}
