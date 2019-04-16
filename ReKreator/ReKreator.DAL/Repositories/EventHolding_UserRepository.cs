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
    public class EventHolding_UserRepository : IRepository<long, EventHolding_User>
    {
        private readonly EventContext _db;

        public EventHolding_UserRepository(EventContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<EventHolding_User>> GetAllAsync()
        {
            return await _db.EventHoldings_Users.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<EventHolding_User>> GetAllAsync(
            Expression<Func<EventHolding_User, bool>> filter,
            Func<IQueryable<EventHolding_User>, IOrderedQueryable<EventHolding_User>> orderBy = null,
            int skip = 0,
            int? take = null,
            params Expression<Func<EventHolding_User, object>>[] includes)
        {
            var eventHoldingUsers = _db.EventHoldings_Users.AsNoTracking().Where(filter);
            foreach (var include in includes)
            {
                eventHoldingUsers = eventHoldingUsers.Include(include);
            }

            if (orderBy != null)
            {
                eventHoldingUsers = orderBy(eventHoldingUsers);
            }

            eventHoldingUsers = eventHoldingUsers.Skip(skip);
            if (take != null)
            {
                eventHoldingUsers = eventHoldingUsers.Take(take.Value);
            }

            return await eventHoldingUsers.ToListAsync();
        }

        public async Task<EventHolding_User> GetAsync(long id)
        {
            return await _db.EventHoldings_Users.FindAsync(id);
        }
        
        public async Task<EventHolding_User> GetAsync(Expression<Func<EventHolding_User, bool>> expression,
            params Expression<Func<EventHolding_User, object>>[] includes)
        {
            IQueryable<EventHolding_User> eventHoldingUsers = _db.EventHoldings_Users;
            foreach (var include in includes)
            {
                eventHoldingUsers = eventHoldingUsers.Include(include);
            }

            return await eventHoldingUsers.FirstOrDefaultAsync(expression);
        }

        public void Create(EventHolding_User item)
        {
            _db.EventHoldings_Users.Add(item);
        }

        public void Delete(long id)
        {
            var item = GetAsync(id);
            if (item != null)
                _db.EventHoldings_Users.Remove(item.Result);
        }

        public void Delete(EventHolding_User item)
        {
            _db.EventHoldings_Users.Remove(item);
        }

        public void Update(EventHolding_User item)
        {
            _db.Attach(item);
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
