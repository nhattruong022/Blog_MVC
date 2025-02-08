using System.ComponentModel.DataAnnotations;

namespace Blog.Models.ViewModels
{
    public class SignUpViewModel
    {


        [Required(ErrorMessage = "Please enter your name")]
        public string Hoten { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [RegularExpression(@"^[a-zA-Z0-9._]+@gmail\.com$",ErrorMessage ="Invalid Email")]
        [EmailAddress]
        public string email { get; set; }

        [Required(ErrorMessage = "Please enter your phone")]
        [RegularExpression(@"^09\d{8}$",ErrorMessage ="Invalid Number")]
        public string phone { get; set; }


        [Key]
        [Required(ErrorMessage ="Please enter your userName")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [MinLength(6,ErrorMessage ="Password has at least 6 characters")]
        public string password {  get; set; }


        public string ? role {  get; set; }

    }
}
