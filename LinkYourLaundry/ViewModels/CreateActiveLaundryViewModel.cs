using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LinkYourLaundry.ViewModels
{
    public class CreateActiveLaundryViewModel
    {
        [Required]
        public int LaundryTemplateId { get; set; }
        [Required]
        public DateTime WashStartTime { get; set; }
    }
}
