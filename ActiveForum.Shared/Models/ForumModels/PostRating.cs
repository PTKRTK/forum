namespace ActiveForum.Shared.Models.ForumModels
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PostRating
    {
        public int Id { get; set; } 
     
        public int Rating { get; set; }
       
        public string UserId { get; set; }
        
        public int PostId { get; set; }
    }
}
