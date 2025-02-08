using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class PostModel
    {
        [Key]
        public int Id { get; set; }

        public string? image {  get; set; }
        public string? Title {  get; set; }
        public string ? Content{ get; set; }
        public int ? Views { get; set; }
        public int ? Tyms {  get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set;}
        public string? status { get; set; }
        public CustomerModel? customer {  get; set; }
    }
}
