namespace ActiveForum.Shared.DTOs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ActiveForum.Shared.Models.ForumModels;

    public class PostDetailsDTO
    {
        public double UserRate { get; set; }

        public double AvgRating { get; set; }

        public Post Post { get; set; }
    }
}
