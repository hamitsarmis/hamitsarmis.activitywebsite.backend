using hamitsarmis.activitywebsite.backend.Entities;
using hamitsarmis.activitywebsite.backend.Interfaces;

namespace hamitsarmis.activitywebsite.backend.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            //return await _context.Users.FindAsync(id);
            throw new NotImplementedException();
        }
    }
}
