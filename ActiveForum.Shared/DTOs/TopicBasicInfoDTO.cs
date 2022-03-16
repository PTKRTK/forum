namespace ActiveForum.Shared.DTOs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ActiveForum.Shared.Models.ForumModels;

    public class TopicBasicInfoDTO
    {
        public Topic Topic { get; set; }      

        public int PostsNumber { get; set; }

        public int UserRate { get; set; }

        public double AvgRating { get; set; }

        public List<CategoryOfTopic> Categories { get; set; }
    }
}
