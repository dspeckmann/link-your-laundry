using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LinkYourLaundry.Services;
using LinkYourLaundry.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LinkYourLaundry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly UserService userService;

        public AuthenticationController(UserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] LoginViewModel viewModel)
        {
            var result = userService.Login(viewModel);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return Forbid();
            }
        }

        [AllowAnonymous]
        [HttpPut]
        public IActionResult Put([FromBody] RefreshViewModel viewModel)
        {
            var result = userService.Refresh(viewModel);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return Forbid();
            }
        }
    }
}