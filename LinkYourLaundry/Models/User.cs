using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkYourLaundry.Models
{
    public class User : IdentityUser<int>
    {
        public int? GroupId { get; set; }
        public Group Group { get; set; }
        public ICollection<LaundryTemplate> LaundryTemplates { get; set; }
        public ICollection<ActiveLaundry> ActiveLaundries { get; set; }
    }
}
