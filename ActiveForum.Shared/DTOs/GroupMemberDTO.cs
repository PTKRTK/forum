namespace ActiveForum.Shared.DTOs
{
    using System.Collections.Generic;
    
    public class GroupMemberDTO
    {
        public int GroupId { get; set; }

        public int MemberId { get; set; }

        public string Email { get; set; }

        public bool IsAccepted { get; set; }
    }
}
