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
    public class EventRepository : IRepository<long, Event>
    {
        private readonly EventContext _db;

        public EventRepository(EventContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _db.Events.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetAllAsync(
            Expression<Func<Event, bool>> filter,
            Func<IQueryable<Event>, IOrderedQueryable<Event>> orderBy = null,
            int skip = 0,
            int? take = null,
            params Expression<Func<Event, object>>[] includes)
        {
            var events = _db.Events.AsNoTracking().Where(filter);
            foreach (var include in includes)
            {
                events = events.Include(include);
            }
            if (orderBy != null)
            {
                events = orderBy(events);
            }
            events = events.Skip(skip);
            if (take != null)
            {
                events = events.Take(take.Value);
            }

            return await events.ToListAsync();
        }

        public async Task<Event> GetAsync(long id)
        {
            return await _db.Events.FindAsync(id);
        }

        public async Task<Event> GetAsync(Expression<Func<Event, bool>> expression, params Expression<Func<Event, object>>[] includes)
        {
            IQueryable<Event> events = _db.Events;
            foreach (var include in includes)
            {
                events = events.Include(include);
            }
            return await events.FirstOrDefaultAsync(expression);
        }

        public void Create(Event item)
        {
            _db.Events.Add(item);
        }

        public void Delete(long id)
        {
            var item = GetAsync(id);
            if (item != null)
                _db.Events.Remove(item.Result);
        }

        public void Delete(Event item)
        {
            _db.Events.Remove(item);
        }

        public void Update(Event item)
        {
            _db.Attach(item);
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
