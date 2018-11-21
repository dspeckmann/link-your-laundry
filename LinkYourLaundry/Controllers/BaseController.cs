using LinkYourLaundry.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace LinkYourLaundry.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected int GetCurrentUserId()
        {
            return int.Parse(User.FindFirst(JwtRegisteredClaimNames.Sub).Value);
        }
    }
}
