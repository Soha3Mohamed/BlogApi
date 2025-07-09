using UserPostApi.Helpers;
using UserPostApi.Models.DTOs.Comment;

namespace UserPostApi.Services.Interfaces
{
    public interface ICommentService
    {
        ServiceResult<List<GetCommentDTO>> GetAllComments();
        ServiceResult<GetCommentDTO> GetComment(int commentId);
        ServiceResult<List<GetCommentDTO>> GetPostComments(int postId);
        ServiceResult<List<GetCommentDTO>> GetUserComments(int userId);
        
        ServiceResult<GetCommentDTO> AddComment(AddCommentDTO addCommentDTO, int postId, int userId);
        ServiceResult<GetCommentDTO> UpdateComment(AddCommentDTO addCommentDTO, int commentId);
        ServiceResult<bool> DeleteComment(int commentId);
        ServiceResult<bool> DeleteUserComment(int userId);

        ServiceResult<bool> DeletePostComments(int postId);
    }
}
