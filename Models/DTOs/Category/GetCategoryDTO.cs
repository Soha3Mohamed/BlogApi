using UserPostApi.Models.DTOs.Post;
using UserPostApi.Models.Entities;

namespace UserPostApi.Models.DTOs.Category
{
    public class GetCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<GetPostDTO> Posts { get; set; } = new List<GetPostDTO>();
    }
}
