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
    public class UserRepository : IRepository<long, User>
    {
        private readonly EventContext _db;

        public UserRepository(EventContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _db.Users.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync(
            Expression<Func<User, bool>> filter,
            Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null,
            int skip = 0,
            int? take = null,
            params Expression<Func<User, object>>[] includes)
        {
            var users = _db.Users.AsNoTracking().Where(filter);
            foreach (var include in includes)
            {
                users = users.Include(include);
            }
            if (orderBy != null)
            {
                users = orderBy(users);
            }
            users = users.Skip(skip);
            if (take != null)
            {
                users = users.Take(take.Value);
            }

            return await users.ToListAsync();
        }

        public async Task<User> GetAsync(long id)
        {
            return await _db.Users.FindAsync(id);
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> expression, params Expression<Func<User, object>>[] includes)
        {
            IQueryable<User> users = _db.Users;
            foreach (var include in includes)
            {
                users = users.Include(include);
            }
            return await users.FirstOrDefaultAsync(expression);
        }

        public void Create(User item)
        {
            _db.Users.Add(item);
        }

        public void Delete(long id)
        {
            var item = GetAsync(id);
            if (item != null)
                _db.Users.Remove(item.Result);
        }

        public void Delete(User item)
        {
            _db.Users.Remove(item);
        }

        public void Update(User item)
        {
            _db.Attach(item);
            _db.Entry(item).State = EntityState.Modified;
            _db.Attach(item.UserMailing);
            _db.Entry(item.UserMailing).State = EntityState.Modified;
        }
    }
}
