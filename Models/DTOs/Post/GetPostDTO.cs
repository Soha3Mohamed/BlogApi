using UserPostApi.Models.DTOs.Comment;

namespace UserPostApi.Models.DTOs.Post
{
    public class GetPostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int CommentCount { get; set; } 
    }
}
