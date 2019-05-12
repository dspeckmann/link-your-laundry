using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkYourLaundry.ViewModels
{
    public class LoginResultViewModel
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
    }
}
