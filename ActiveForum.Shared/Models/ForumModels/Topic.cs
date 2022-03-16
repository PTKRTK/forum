namespace ActiveForum.Shared.Models.ForumModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Topic
    {
        public int Id { get; set; }
        
        public string UserName { get; set; }

        public int Status { get; set; }

        public string Title { get; set; }

        public string TopicContent { get; set; }

        public DateTime CreationDate { get; set; }

        public int SportId { get; set; }

        public List<Topic_Category> Topic_Categories { get; set; }
    }
}
