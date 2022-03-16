namespace ActiveForum.Shared.Models.GroupModels
{
    using System.Collections.Generic;
    
    public class GroupMember
    {
        public int Id { get; set; }

        public bool Accepted { get; set; }

        public string UserName { get; set; }

        public int PrivateGroupId { get; set; }

        public List<GroupRole> GroupRoles { get; set; }
    }
}
