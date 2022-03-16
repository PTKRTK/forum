namespace ActiveForum.Shared.Models.GroupModels
{
    using System;
    using System.Collections.Generic;

    public class GroupTopic
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string TopicContent { get; set; }

        public DateTime CreationDate { get; set; }

        public int GroupMemberId { get; set; }

        public string UserName { get; set; }

        public int GroupPostsAmount { get; set; }
    }
}
