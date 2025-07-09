namespace UserPostApi.Models.DTOs.Comment
{
    public class GetCommentDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }
    }
}
