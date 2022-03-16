namespace ActiveForum.Shared.Models.GroupModels
{
    public class PrivateGroup
    {
        public int Id { get; set; }

        public string GroupName { get; set; }

        public string Description { get; set; }

        public bool GroupVisible { get; set; }

        public bool IsPrivate { get; set; }
    }
}
