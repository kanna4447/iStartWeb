
using System.ComponentModel.DataAnnotations;

namespace iStartWeb.Models
{
    public class UserLoginViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$", ErrorMessage = "Invalid pattern.")]
        
        public string EmailID { get; set; }

        [Required]
        public string Password { get; set; }
    }
}