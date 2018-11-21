using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkYourLaundry.Models
{
    public class User : IdentityUser<int>
    {
        public int? GroupOwnerId { get; set; }
        public virtual User GroupOwner { get; set; }
        public virtual ICollection<LaundryTemplate> LaundryTemplates { get; set; }
        public virtual ICollection<ActiveLaundry> ActiveLaundries { get; set; }
        public virtual ICollection<User> GroupMembers { get; set; }
        public virtual ICollection<Invitation> PendingActiveInvitations { get; set; }
        public virtual ICollection<Invitation> PendingPassiveInvitations { get; set; }
    }
}
