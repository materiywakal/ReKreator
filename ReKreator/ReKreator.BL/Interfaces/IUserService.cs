using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ReKreator.Domain;

namespace ReKreator.BL.Interfaces
{
    public interface IUserService
    {
        Task<User> GetAsync(Expression<Func<User, bool>> expression, params Expression<Func<User, object>>[] includes);
    }
}
