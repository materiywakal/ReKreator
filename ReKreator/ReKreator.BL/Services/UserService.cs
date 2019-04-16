using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ReKreator.BL.Interfaces;
using ReKreator.DAL.Interfaces;
using ReKreator.Domain;

namespace ReKreator.BL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _db;
        private readonly IRepository<long, User> _repository;

        public UserService(IUnitOfWork uow, IRepository<long, User> repository)
        {
            _db = uow ?? throw new ArgumentNullException(nameof(uow));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<User> GetAsync(Expression<Func<User, bool>> expression, params Expression<Func<User, object>>[] includes)
        {
            return _repository.GetAsync(expression, includes);
        }
    }
}
