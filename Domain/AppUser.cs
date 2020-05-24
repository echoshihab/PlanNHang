using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        // eager loading- public ICollection<UserActivity> UserActivities { get; set; }
        public virtual ICollection<UserActivity> UserActivities { get; set; }

    }
}