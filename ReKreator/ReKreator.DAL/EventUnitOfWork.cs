using Microsoft.EntityFrameworkCore;
using ReKreator.DAL.Interfaces;
using ReKreator.DAL.Repositories;
using ReKreator.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ReKreator.DAL
{
    public class EventUnitOfWork : IUnitOfWork
    {
        private readonly EventContext _context;
        private IRepository<long, User> _userRepository;
        private IRepository<long, Event> _eventRepository;
        private IRepository<long, EventPlace> _eventPlaceRepository;
        private IRepository<long, EventHolding> _eventHoldingRepository;
        private IRepository<long, EventHolding_User> _eventHolding_UserRepository;

        public bool _isDisposed;

        public EventUnitOfWork(EventContext context)
        {
            _context = context;
        }

        public IRepository<long, User> UserRepository =>
            _userRepository ?? (_userRepository = new UserRepository(_context));

        public IRepository<long, Event> EventRepository =>
            _eventRepository ?? (_eventRepository = new EventRepository(_context));

        public IRepository<long, EventPlace> EventPlaceRepository =>
            _eventPlaceRepository ?? (_eventPlaceRepository = new EventPlaceRepository(_context));

        public IRepository<long, EventHolding> EventHoldingRepository =>
            _eventHoldingRepository ?? (_eventHoldingRepository = new EventHoldingRepository(_context));

        public IRepository<long, EventHolding_User> EventHolding_UserRepository =>
            _eventHolding_UserRepository ?? (_eventHolding_UserRepository = new EventHolding_UserRepository(_context));

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _isDisposed = true;
            }
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void RejectChanges()
        {
            foreach (var entry in _context.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}