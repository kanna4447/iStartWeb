
using System.ComponentModel.DataAnnotations;

namespace iStartWeb.Models
{
    public class UserRegisterViewModel
    {
        public int RegisterId { get; set; }
        [Required]
        [StringLength(75, ErrorMessage = "Name should have only alphabets Max length of {0} and Min length of {1}", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$", ErrorMessage = "Invalid pattern.")]
        [Display(Name = "Email ID")]
        public string EmailID { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public string ReEnterPassword { get; set; }
    }
}