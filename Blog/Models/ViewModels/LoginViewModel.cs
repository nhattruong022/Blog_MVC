using System.ComponentModel.DataAnnotations;

namespace Blog.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Please enter your userName")]
        public string userName {  get; set; }


        [Required(ErrorMessage ="Please enter your password")]
        [MinLength(6, ErrorMessage = "Password has at least 6 characters")]
        public string password { get; set; }


    }
}
