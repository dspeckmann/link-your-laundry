using LinkYourLaundry.Models;
using LinkYourLaundry.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LinkYourLaundry.Services
{
    public class InvitationService
    {
        private readonly LaundryDbContext context;
        private readonly UserService userService;

        public InvitationService(LaundryDbContext context, UserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public async Task<Invitation> AddInvitation(InvitationViewModel viewModel, int groupOwnerId)
        {
            /*
            var invitation = new Invitation
            {
                GroupOwnerId = groupOwnerId,
                InvitedUser = userService.GetByEmail(viewModel.Email)
            };

            await context.Invitations.AddAsync(invitation);
            */

            var invitedUser = userService.GetByEmail(viewModel.Email);
            var invitation = new Invitation { GroupOwnerId = groupOwnerId };
            invitedUser.PendingPassiveInvitations.Add(invitation);


            await context.SaveChangesAsync();

            return invitation;
        }

        public async Task<Invitation> DeleteInvitation(Invitation invitation)
        {
            context.Invitations.Remove(invitation);
            await context.SaveChangesAsync();

            return invitation;
        }

        public async Task<Invitation> DeleteInvitation(int id)
        {
            var invitation = context.Invitations.Find(id);

            return await DeleteInvitation(invitation);
        }
    }
}
