using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ng_server.Models
{
    public class UserRegistration
    {
        [Required]
        [RegularExpression(@"^[a-zA-ZçğıöşüÇĞİÖŞÜ\s]+$", ErrorMessage = "Only letters and spaces are allowed.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
    }
}