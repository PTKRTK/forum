namespace ActiveForum.Shared.DTOs
{
    using System.Collections.Generic;
    
    public class VerifiedDTO
    {
        public int TopicID { get; set; }

        public List<int> PostIds { get; set; }
    }
}
