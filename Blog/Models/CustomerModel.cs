using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class CustomerModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter your Name")]
        public string? Name { get; set; }

        [Required(ErrorMessage ="Please enter your Email")]
        public string? email {  get; set; }

        [Required(ErrorMessage ="Please enter your phone")]
        public string? phone { get; set; }
        
        [Required(ErrorMessage ="Please enter your UserName")]
        public string? userName {  get; set; }

        [Required(ErrorMessage ="Please enter your password")]   
        public string? password { get; set; }
        public string? role { get; set; }
    }
}
