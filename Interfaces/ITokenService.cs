using hamitsarmis.activitywebsite.backend.Entities;

namespace hamitsarmis.activitywebsite.backend.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
