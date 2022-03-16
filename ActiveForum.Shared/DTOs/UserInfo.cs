namespace ActiveForum.Shared.DTOs
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UserInfo
    {
        [Required(ErrorMessage = "Musisz podać email")]
        [StringLength(60, MinimumLength = 7, ErrorMessage = "Email musi zawierać od 7 do 60 liter")]
        public string Email { get; set; }

        [StringLength(42, MinimumLength = 6, ErrorMessage = "Hasło musi zawierać od 6 do 42 liter")]
        [Required(ErrorMessage = "Musisz podać hasło")]
        public string Password { get; set; }
    }
}
