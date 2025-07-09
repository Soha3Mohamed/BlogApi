using System.ComponentModel.DataAnnotations;

namespace UserPostApi.Models.DTOs.Post
{
    public class AddPostDTO
    {
        [Required]
        public string Title { get; set; }
        [StringLength(1000, MinimumLength = 10, ErrorMessage ="Description must be between 10 and 1000 characters")]
        public string Description { get; set; }

       
        [Range(1,int.MaxValue,ErrorMessage ="Post Id must be positive")]
        public int UserId { get; set; }
    }
}
