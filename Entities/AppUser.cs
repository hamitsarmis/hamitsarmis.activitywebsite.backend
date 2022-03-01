using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace hamitsarmis.activitywebsite.backend.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public DateTime Created { get; set; } = DateTime.Now;

        public ICollection<AppUserRole> UserRoles { get; set; }

    }
}
