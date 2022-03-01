using System.Threading.Tasks;
using hamitsarmis.activitywebsite.backend.Interfaces;

namespace hamitsarmis.activitywebsite.backend.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository => new UserRepository(_context);

        public IEventRepository EventRepository => new EventRepository(_context);

        public IMealRepository MealRepository => new MealRepository(_context);

    }
}