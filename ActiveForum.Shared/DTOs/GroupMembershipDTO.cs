namespace ActiveForum.Shared.DTOs
{
    using System.Collections.Generic;

    public class GroupMembershipDTO
    {
        public int GroupId { get; set; }

        public int MemberId { get; set; }

        public string UserId { get; set; }

        public List<string> RoleNames { get; set; }
    }
}
