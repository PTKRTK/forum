namespace ActiveForum.Shared.DTOs
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TopicsFilterDTO
    {
        public int Status { get; set; }
        
        public List<int> Categories { get; set; }

        public string Title { get; set; }
    }
}
