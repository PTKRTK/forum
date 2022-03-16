namespace ActiveForum.Server.SignalR.Hubs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ActiveForum.Database.DbContexts;
    using ActiveForum.Database.Entities.FriendshipAndMessagesEntities;
    using ActiveForum.Shared.Models.CommunicatorModels;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.SignalR;

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub : Hub
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ActiveForumDbContext context;

        public ChatHub(
            ActiveForumDbContext context, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public override async Task OnConnectedAsync()
        {
            var user = await userManager.FindByEmailAsync(Context.User.Identity.Name);
            var userName = user.UserName;

          /*  await SendMessage(new MessageModel()
            {
                RecipientEmail = string.Empty,
                SenderEmail = userName,
                Message = "Connected",
                Date = DateTime.Now
            });*/
            await base.OnConnectedAsync();
        }

        public async Task SendMessage(MessageModel messageModel)
        {
            var user = await userManager.FindByEmailAsync(Context.User.Identity.Name);
            messageModel.SenderEmail = user.UserName;
            messageModel.Date = DateTime.Now;

            string recipentId = context.Users.Where(x => x.Email == messageModel.RecipientEmail).Select(x => x.Id).FirstOrDefault();
            string userId = user.Id;
            var friendshipId = context.Friendships.Where(x => (x.AskingUserId == recipentId && x.InvitedUserId == userId) 
                                || (x.AskingUserId == userId && x.InvitedUserId == recipentId)).Select(x => x.Id).FirstOrDefault();

            if (friendshipId != 0)
            {
                FriendMessageEntity friendMessage = new ()
                {
                    FriendshipId = friendshipId,
                    SenderEmail = user.UserName,
                    RecipientEmail = messageModel.RecipientEmail,
                    Message = messageModel.Message,
                    CreationDate = messageModel.Date
                };

                context.Add(friendMessage);
                await context.SaveChangesAsync();
            }

            var users = new string[] { user.Email, messageModel.RecipientEmail };
            await Clients.Users(users).SendAsync("GetMessage", messageModel);
        }
    }
}
