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
    public class EventPlaceRepository : IRepository<long, EventPlace>
    {
        private readonly EventContext _db;

        public EventPlaceRepository(EventContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<EventPlace>> GetAllAsync()
        {
            return await _db.EventPlaces.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<EventPlace>> GetAllAsync(
            Expression<Func<EventPlace, bool>> filter,
            Func<IQueryable<EventPlace>, IOrderedQueryable<EventPlace>> orderBy = null,
            int skip = 0,
            int? take = null,
            params Expression<Func<EventPlace, object>>[] includes)
        {
            var eventPlaces = _db.EventPlaces.AsNoTracking().Where(filter);
            foreach (var include in includes)
            {
                eventPlaces = eventPlaces.Include(include);
            }
            if (orderBy != null)
            {
                eventPlaces = orderBy(eventPlaces);
            }
            eventPlaces = eventPlaces.Skip(skip);
            if (take != null)
            {
                eventPlaces = eventPlaces.Take(take.Value);
            }

            return await eventPlaces.ToListAsync();
        }

        public async Task<EventPlace> GetAsync(long id)
        {
            return await _db.EventPlaces.FindAsync(id);
        }

        public async Task<EventPlace> GetAsync(Expression<Func<EventPlace, bool>> expression, params Expression<Func<EventPlace, object>>[] includes)
        {
            IQueryable<EventPlace> eventPlaces = _db.EventPlaces;
            foreach (var include in includes)
            {
                eventPlaces = eventPlaces.Include(include);
            }
            return await eventPlaces.FirstOrDefaultAsync(expression);
        }

        public void Create(EventPlace item)
        {
            _db.EventPlaces.Add(item);
        }

        public void Delete(long id)
        {
            var item = GetAsync(id);
            if (item != null)
                _db.EventPlaces.Remove(item.Result);
        }

        public void Delete(EventPlace item)
        {
            _db.EventPlaces.Remove(item);
        }

        public void Update(EventPlace item)
        {
            _db.Attach(item);
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
