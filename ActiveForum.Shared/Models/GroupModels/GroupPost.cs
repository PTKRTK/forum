namespace ActiveForum.Shared.Models.GroupModels
{
    using System;
    
    public class GroupPost
    {
        public int Id { get; set; }
        
        public int GroupMemberId { get; set; }

        public string PostContent { get; set; }

        public DateTime CreationDate { get; set; }

        public int GroupTopicId { get; set; }

        public string CreatorName { get; set; }
    }
}
