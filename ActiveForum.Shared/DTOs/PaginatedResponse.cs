namespace ActiveForum.Shared.DTOs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    public class PaginatedResponse<T>
    {
        public T Response { get; set; }
       
        public int PagesAmount { get; set; }
    }
}
