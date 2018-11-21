using LinkYourLaundry.Models;
using LinkYourLaundry.Services;
using LinkYourLaundry.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkYourLaundry.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationsController : BaseController
    {
        private readonly InvitationService invitationService;

        public InvitationsController(InvitationService invitationService)
        {
            this.invitationService = invitationService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InvitationViewModel viewModel)
        {
            var invitation = await invitationService.AddInvitation(viewModel, GetCurrentUserId());
            return Ok(invitation);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var invitation = await invitationService.DeleteInvitation(id);
            return Ok(invitation);
        }
    }
}
