namespace ActiveForum.Shared.Models.CommunicatorModels
{
    using System;

    public class MessageModel
    {
        public string RecipientEmail { get; set; }

        public string SenderEmail { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }
    }
}
