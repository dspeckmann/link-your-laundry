using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkYourLaundry.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
