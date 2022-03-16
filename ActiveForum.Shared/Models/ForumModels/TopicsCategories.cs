namespace ActiveForum.Shared.Models.ForumModels
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TopicsCategories
    {
        public int TopicId { get; set; }

        public int TopicCategoryId { get; set; }
        
        public CategoryOfTopic CategoryOfTopic { get; set; }

        public Topic Topic { get; set; }
    }
}
