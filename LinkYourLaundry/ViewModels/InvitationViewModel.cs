using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LinkYourLaundry.ViewModels
{
    public class InvitationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
