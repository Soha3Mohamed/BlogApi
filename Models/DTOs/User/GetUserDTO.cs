using UserPostApi.Models.DTOs.Post;
using UserPostApi.Models.Entities;

namespace UserPostApi.Models.DTOs.User
{
    public class GetUserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public List<GetPostDTO> Posts { get; set; } = new List<GetPostDTO>();
    }
}
