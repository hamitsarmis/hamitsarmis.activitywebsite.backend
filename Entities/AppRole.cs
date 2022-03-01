using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace hamitsarmis.activitywebsite.backend.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}