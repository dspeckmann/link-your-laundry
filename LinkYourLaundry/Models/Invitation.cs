using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkYourLaundry.Models
{
    public class Invitation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int GroupOwnerId { get; set; }
        public virtual User GroupOwner { get; set; }
        public int InvitedUserId { get; set; }
        public virtual User InvitedUser { get; set; }
    }
}
