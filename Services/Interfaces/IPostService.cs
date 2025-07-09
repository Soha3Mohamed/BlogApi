using UserPostApi.Helpers;
using UserPostApi.Models.DTOs.Post;

namespace UserPostApi.Services.Interfaces
{
    public interface IPostService
    {
        ServiceResult<List<GetPostDTO>> GetAllPosts();
        ServiceResult<GetPostDTO> GetPost(int postId, int userId);
        ServiceResult<List<GetPostDTO>> GetUserPosts(int userId);
        
        ServiceResult<GetPostDTO> AddPost(AddPostDTO addPostDTO, int userId);
        ServiceResult<GetPostDTO> UpdatePost(AddPostDTO addPostDTO, int postId);
        ServiceResult<bool> DeletePost(int id);
        //bool DeletePostComments(int postId); in comment service
        ServiceResult<bool> DeleteUserPosts(int userId);
    }
}
