using System.ComponentModel.DataAnnotations;
using UserPostApi.Models.Entities;

namespace UserPostApi.Models.DTOs.Comment
{
    public class AddCommentDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Title must be no more than 100 characters.")]
        [MinLength(10 , ErrorMessage ="Title must be at least 10 characters.")]
        public string Title { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 10, ErrorMessage ="Content must be between 10 and 1000 characters.")]
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; }

        [Required(ErrorMessage ="UserId is required.")]
        [Range(1,int.MaxValue, ErrorMessage ="UserId must be a positive number.")]
        public int UserId { get; set; }

        [Required(ErrorMessage ="PostId is required.")]
        [Range(1,int.MaxValue, ErrorMessage ="PostId must be a positive number.")]
        public int PostId { get; set; }
        
    }
}
