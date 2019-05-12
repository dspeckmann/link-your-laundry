using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkYourLaundry.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
        public virtual User User { get;set; }
    }
}
