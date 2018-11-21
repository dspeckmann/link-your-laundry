using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkYourLaundry.Models
{
    public class ActiveLaundry
    {
        public int Id { get; set; }
        public DateTime WashStartTime { get; set; }
        public DateTime DryStartTime { get; set; }
        public bool Completed { get; set; }
        public int LaundryTemplateId { get; set; }
        public virtual LaundryTemplate LaundryTemplate { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
