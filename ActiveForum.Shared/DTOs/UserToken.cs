namespace ActiveForum.Shared.DTOs
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    
public class UserToken
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }
    }
}
