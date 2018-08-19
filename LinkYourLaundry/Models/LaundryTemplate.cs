using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkYourLaundry.Models
{
    public class LaundryTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detergent { get; set; }
        public string WashCycle { get; set; }
        public TimeSpan WashDuration { get; set; }
        public string DryCycle { get; set; }
        public TimeSpan DryDuration { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<ActiveLaundry> ActiveLaundries { get; set; }
    }
}
