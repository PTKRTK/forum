namespace ActiveForum.Shared.Models.GroupModels
{
    public class GroupRole
    {
        public string Id { get; set; }

        public string Name { get; set; } 
        
        public int GroupMemberId { get; set; }
    }
}
