using hamitsarmis.activitywebsite.backend.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hamitsarmis.activitywebsite.backend.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser> GetUserByIdAsync(int id);
    }
}