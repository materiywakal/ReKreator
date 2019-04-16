using ReKreator.Domain;
using System;
using System.Threading.Tasks;

namespace ReKreator.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<long, User> UserRepository { get; }
        IRepository<long, Event> EventRepository { get; }
        IRepository<long, EventPlace> EventPlaceRepository { get; }
        IRepository<long, EventHolding> EventHoldingRepository { get; }
        IRepository<long, EventHolding_User> EventHolding_UserRepository { get; }

        Task SaveAsync();
        void RejectChanges();
    }
}