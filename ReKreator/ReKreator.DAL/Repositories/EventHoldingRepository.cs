using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReKreator.DAL.Interfaces;
using ReKreator.Domain;

namespace ReKreator.DAL.Repositories
{
    public class EventHoldingRepository : IRepository<long, EventHolding>
    {
        private readonly EventContext _db;

        public EventHoldingRepository(EventContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<EventHolding>> GetAllAsync()
        {
            return await _db.EventHoldings.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<EventHolding>> GetAllAsync(
            Expression<Func<EventHolding, bool>> filter,
            Func<IQueryable<EventHolding>, IOrderedQueryable<EventHolding>> orderBy = null,
            int skip = 0,
            int? take = null,
            params Expression<Func<EventHolding, object>>[] includes)
        {
            var eventHoldings = _db.EventHoldings.AsNoTracking().Where(filter);
            foreach (var include in includes)
            {
                eventHoldings = eventHoldings.Include(include);
            }

            if (orderBy != null)
            {
                eventHoldings = orderBy(eventHoldings);
            }

            eventHoldings = eventHoldings.Skip(skip);
            if (take != null)
            {
                eventHoldings = eventHoldings.Take(take.Value);
            }

            return await eventHoldings.ToListAsync();
        }

        public async Task<EventHolding> GetAsync(long id)
        {
            return await _db.EventHoldings.FindAsync(id);
        }
        
        public async Task<EventHolding> GetAsync(Expression<Func<EventHolding, bool>> expression,
            params Expression<Func<EventHolding, object>>[] includes)
        {
            IQueryable<EventHolding> eventHoldings = _db.EventHoldings;
            foreach (var include in includes)
            {
                eventHoldings = eventHoldings.Include(include);
            }

            return await eventHoldings.FirstOrDefaultAsync(expression);
        }

        public void Create(EventHolding item)
        {
            _db.EventHoldings.Add(item);
        }

        public void Delete(long id)
        {
            var item = GetAsync(id);
            if (item != null)
                _db.EventHoldings.Remove(item.Result);
        }

        public void Delete(EventHolding item)
        {
            _db.EventHoldings.Remove(item);
        }

        public void Update(EventHolding item)
        {
            _db.Attach(item);
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
