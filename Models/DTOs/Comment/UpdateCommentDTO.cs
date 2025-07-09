using System.ComponentModel.DataAnnotations;

namespace UserPostApi.Models.DTOs.Comment
{
    public class UpdateCommentDTO
    {
        public string Title { get; set; }
        [MinLength(10)]
        public string Content { get; set; }
        
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

       
    }
}
