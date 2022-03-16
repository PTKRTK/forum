namespace ActiveForum.Shared.DTOs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    public class PaginationDTO
    {
        public int CurrentPage { get; set; } = 1;
       
        public int PageObjectsNumber { get; set; } = 2;
    }
}
