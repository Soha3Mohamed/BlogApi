using UserPostApi.Models.DTOs.Category;
using UserPostApi.Models.DTOs.Post;

namespace UserPostApi.Services.Interfaces
{
    public interface ICategoryService
    {

        List<GetCategoryDTO> GetAllCategories();
        GetCategoryDTO GetCategory(int id);

        GetCategoryDTO AddCategory(AddCategoryDTO addCategoryDTO);

        GetCategoryDTO UpdateCategory(AddCategoryDTO addCategoryDTO, int categoryId);

        List<GetPostDTO> GetPostsByCategoryId(int categoryId);

        bool DeleteCategory(int id);

        bool DeleteCategoryPosts(int categoryId);
    }
}
