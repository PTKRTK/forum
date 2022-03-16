namespace ActiveForum.Server.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ActiveForum.Database.DbContexts;
    using ActiveForum.Database.Entities.FriendshipAndMessagesEntities;
    using ActiveForum.Shared.DTOs;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("api/friends")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class FriendsController : ControllerBase
    {
        private readonly ActiveForumDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public FriendsController(
            ActiveForumDbContext context,
            UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
       
        [HttpPost("invite")]
        public async Task<ActionResult<ActionMessage>> Invite(FriendInfo friendInfo)
        {
            var actionMessage = new ActionMessage()
            {
                Successful = false
            };
            var user = await userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            var userId = user.Id;

            if (friendInfo.Email == user.Email)
            {
                actionMessage.CommunicateContent = "Nie możesz zaprosić samego siebie, wpisz email innego użytkownika";
                return actionMessage;
            }
            else
            {        
            var invitedUserId = context.Users.Where(x => x.Email == friendInfo.Email).Select(x => x.Id).FirstOrDefault();

            bool notExists = context.Friendships.Where(x => (x.AskingUserId == userId && x.InvitedUserId == invitedUserId) || (x.AskingUserId == invitedUserId && x.InvitedUserId == userId)).Any();
            bool invitationExist = context.Friendships.Where(x => (x.AskingUserId == userId && x.InvitedUserId == invitedUserId)).Any();
            bool userInvited = context.Friendships.Where(x => (x.AskingUserId == invitedUserId && x.InvitedUserId == userId)).Any();

            if (notExists == false && !string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(invitedUserId))
            {
                FriendshipEntity friendship = new ()
                {
                    Accepted = false,
                    AskingUserId = userId,
                    InvitedUserId = invitedUserId
                };

                context.Add(friendship);
                await context.SaveChangesAsync();
                actionMessage.Successful = true;
                actionMessage.CommunicateContent = "Wysłano zaproszenie";
                return actionMessage;
            }
           
            if (invitationExist == true)
            {
                actionMessage.CommunicateContent = "Już wysłałeś zaproszenie";
                return actionMessage;
            }

            if (userInvited == true)
            {
                actionMessage.CommunicateContent = "Już zostałeś zaproszony przez tego użytkownika. Sprawdź zaproszenia i zakceptuj w celu dodania do znajomych.";
                return actionMessage;
            }

            actionMessage.CommunicateContent = "Wystąpił błąd. Sprawdź czy poprawnie wpisałeś mail użytkownika";
            return actionMessage;
            }
        }

        [HttpPut("accept")]
        public async Task<ActionResult> AcceptInvite(FriendInfo friendInfo)
        {
            var x = friendInfo;
            try
            {
                var user = await userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
                var userId = user.Id;
                var askingUserId = context.Users.Where(x => x.Email == friendInfo.Email).Select(x => x.Id).FirstOrDefault();

                var friendship = await context.Friendships.Where(x => x.AskingUserId == askingUserId && x.InvitedUserId == userId).FirstOrDefaultAsync();

                friendship.Accepted = true;
                
                context.Attach(friendship).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch
            {
            }

            return NoContent();
        }

        [HttpDelete("delete/{exFriendMail}")]
        public async Task<ActionResult> RemoveFriendship(string exFriendMail)
        {
            var user = await userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            var userId = user.Id;
            var exFriendId = context.Users.Where(x => x.Email == exFriendMail).Select(x => x.Id).FirstOrDefault();

            var friendship = await context.Friendships
                .Where(x => (x.AskingUserId == userId && x.InvitedUserId == exFriendId) || (x.AskingUserId == exFriendId && x.InvitedUserId == userId)).FirstOrDefaultAsync();

            var messages = context.FriendMessages.Where(x => x.FriendshipId == friendship.Id).ToList();
            messages.ForEach(x => context.Remove(x));

            if (friendship == null)
            {
                return NotFound();
            }

            context.Remove(friendship);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("getFriends")]
        public async Task<ActionResult<List<FriendInfo>>> GetFriends()
        {
            List<FriendInfo> requestersInfo = new ();
            var user = await userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            var userId = user.Id;
            Boolean accepted = true;
            var askingUsersIds = context.Friendships.Where(x => x.InvitedUserId == userId && x.Accepted == accepted).Select(x => x.AskingUserId).ToList();

            var invitedUsersIds = context.Friendships.Where(x => x.AskingUserId == userId && x.Accepted == accepted).Select(x => x.InvitedUserId).ToList();

            List<string> userFriendsIds = askingUsersIds.Union(invitedUsersIds).ToList();

            var userFriendsMails = context.Users.Where(x => userFriendsIds.Contains(x.Id)).Select(x => x.Email).ToList();

            foreach (var a in userFriendsMails)
            {
                requestersInfo.Add(new FriendInfo()
                {
                    Email = a
                });
            }

            return requestersInfo;
        }
        
        [HttpGet("requests")]
        public async Task<ActionResult<List<FriendInfo>>> GetRequests()
        {
            List<FriendInfo> requestersInfo = new ();
            var user = await userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            var userId = user.Id;
            Boolean notAccepted = false; 
            var askingUsersIds = context.Friendships.Where(x => x.InvitedUserId == userId && x.Accepted == notAccepted).Select(x => x.AskingUserId).ToList();

            var askingUsersMails = context.Users.Where(x => askingUsersIds.Contains(x.Id)).Select(x => x.Email).ToList();

            foreach (var a in askingUsersMails)
            {
                requestersInfo.Add(new FriendInfo()
                {
                    Email = a
                }); 
            }

            return requestersInfo;
        }
    }
}
