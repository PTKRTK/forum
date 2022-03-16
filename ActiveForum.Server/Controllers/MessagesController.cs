namespace ActiveForum.Server.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ActiveForum.Database.DbContexts;
    using ActiveForum.Shared.Models.CommunicatorModels;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/messages")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MessagesController : ControllerBase
    {
        private readonly ActiveForumDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public MessagesController(
            ActiveForumDbContext context,
            UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpGet("getMessages/{friendMail}")]
        public async Task<ActionResult<List<MessageModel>>> GetUserRating(string friendMail)
        {
            var user = await userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            var userId = user.Id;

            string recipentId = context.Users.Where(x => x.Email == friendMail).Select(x => x.Id).FirstOrDefault();
            var friendshipId = context.Friendships.Where(x => (x.AskingUserId == recipentId && x.InvitedUserId == userId)
                                || (x.AskingUserId == userId && x.InvitedUserId == recipentId)).Select(x => x.Id).FirstOrDefault();

            var friendMessagesEntity = context.FriendMessages.Where(x => x.FriendshipId == friendshipId).ToList();

            var friendMessages = friendMessagesEntity.Select(x => new MessageModel()
            {
                Date = x.CreationDate,
                Message = x.Message,
                RecipientEmail = x.RecipientEmail,
                SenderEmail = x.SenderEmail
            }).ToList();

            return friendMessages;
        }
    }
}
