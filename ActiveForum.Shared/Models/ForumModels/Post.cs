namespace ActiveForum.Shared.Models.ForumModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    public class Post
    {
        public int Id { get; set; }

        public int Status { get; set; }

        public string PostContent { get; set; }

        public DateTime CreationDate { get; set; }

        public int TopicId { get; set; }

        public string UserName { get; set; }
    }
}
