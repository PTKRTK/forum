namespace ActiveForum.Shared.Models.ForumModels
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    
public class TopicRating
    {
        public int Id { get; set; }
        
        public int Rating { get; set; }
        
        public string UserId { get; set; }
        
        public int TopicId { get; set; }
    }
}
