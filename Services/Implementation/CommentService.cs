using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using UserPostApi.Helpers;
using UserPostApi.Models.Data;
using UserPostApi.Models.DTOs.Comment;
using UserPostApi.Models.Entities;
using UserPostApi.Services.Interfaces;

namespace UserPostApi.Services.Implementation
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _dbContext;

        public CommentService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public ServiceResult<List<GetCommentDTO>> GetAllComments()
        {
            var allComments = _dbContext.Comments.ToList();
            if (allComments.Any())
            {
                var allCommentsDTO = allComments.Select(C => new GetCommentDTO
                {
                    Title = C.Title,
                    Content = C.Content,
                    CreatedDate = C.CreatedDate,
                    UpdatedDate = C.UpdatedDate,
                    UserId = C.UserId,
                    PostId = C.PostId
                }).ToList();
                return ServiceResult<List<GetCommentDTO>>.Ok(allCommentsDTO);
            }
            return null;
        }//done sr

        public ServiceResult<GetCommentDTO> GetComment(int commentId)
        {
            var comment = _dbContext.Comments.Find(commentId);
            if (comment == null)
            {
                return ServiceResult<GetCommentDTO>.Fail($"Comment with id: {commentId} is not found");
            }
            var commentDTO = new GetCommentDTO
            {
                Title = comment.Title,
                Content = comment.Content,
                CreatedDate = comment.CreatedDate,
                UpdatedDate = comment.UpdatedDate,
                UserId = comment.UserId,
                PostId = comment.PostId
            };
            return ServiceResult<GetCommentDTO>.Ok(commentDTO);
        }

        public ServiceResult<List<GetCommentDTO>> GetPostComments(int postId)
        {
            var post = _dbContext.Posts.Include(P => P.Comments).FirstOrDefault(post => post.Id == postId);
            if (post == null)
            {
                return ServiceResult<List<GetCommentDTO>>.Fail($"Post with id: {postId} is not found");
            }
            var postCommentsDTO = post.Comments.Select(C => new GetCommentDTO
            {
                Title = C.Title,
                Content = C.Content,
                CreatedDate = C.CreatedDate,
                UpdatedDate = C.UpdatedDate,
                UserId = C.UserId,
                PostId = postId
            }).ToList();
            if (postCommentsDTO.Any())
            {
                return ServiceResult<List<GetCommentDTO>>.Fail($"There are no comments on this post with id: {postId}");
            }
            return ServiceResult<List<GetCommentDTO>>.Ok(postCommentsDTO);
        }//done

        public ServiceResult<List<GetCommentDTO>> GetUserComments(int userId)
        {
            var user = _dbContext.Users.Include(U => U.Comments).FirstOrDefault(user => user.Id == userId);
            if (user == null)
            {
                return ServiceResult<List<GetCommentDTO>>.Fail($"User with id: {userId} is not found");
            }
            var userCommentsDTO = user.Comments.Select(C => new GetCommentDTO
            {
                Title = C.Title,
                Content = C.Content,
                CreatedDate = C.CreatedDate,
                UpdatedDate = C.UpdatedDate,
                UserId = C.UserId,
                PostId = C.PostId
            }).ToList();
            if (!userCommentsDTO.Any())
            {
                return ServiceResult<List<GetCommentDTO>>.Fail($"User with id: {userId} doesn't have comments yet");
            }
            return ServiceResult<List<GetCommentDTO>>.Ok(userCommentsDTO);
        }//done

        public ServiceResult<GetCommentDTO> AddComment(AddCommentDTO addCommentDTO, int postId, int userId)
        {
            var comment = new Comment
            {
                Title = addCommentDTO.Title,
                Content = addCommentDTO.Content,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                UserId = userId,
                PostId = postId
            };
            _dbContext.Comments.Add(comment);
            _dbContext.SaveChanges();

            var commentDTO = new GetCommentDTO
            {
                Title = comment.Title,
                Content = comment.Content,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                UserId = comment.UserId,
                PostId = comment.PostId
            };

            return ServiceResult<GetCommentDTO>.Ok(commentDTO);
        }//done

        public ServiceResult<GetCommentDTO> UpdateComment(AddCommentDTO addCommentDTO, int commentId)
        {
            var comment = _dbContext.Comments.Find(commentId);

            if (comment == null)
            {
                return ServiceResult<GetCommentDTO>.Fail($"Comment with id: {commentId} is not found");
            }

            if (comment.UserId != addCommentDTO.UserId) //good move?
            {
                return ServiceResult<GetCommentDTO>.Fail($"The user id {addCommentDTO.UserId} doesn't match");
            }
            if (comment.PostId != addCommentDTO.PostId)
            {
                return ServiceResult<GetCommentDTO>.Fail($"The post id {addCommentDTO.PostId} doesn't match");
            }

            comment.Title = addCommentDTO.Title;
            comment.Content = addCommentDTO.Content;
            comment.UpdatedDate = DateTime.Now;
            //comment.UserId = addCommentDTO.UserId; //not allowed to change
            //comment.PostId = addCommentDTO.PostId;


            _dbContext.SaveChanges();

            var commentDTO = new GetCommentDTO
            {
                Title = comment.Title,
                Content = comment.Content,
                CreatedDate = comment.CreatedDate,
                UpdatedDate = comment.UpdatedDate,
                UserId = comment.UserId,
                PostId = comment.PostId
            };
            return ServiceResult<GetCommentDTO>.Ok(commentDTO);
        }//done

        public ServiceResult<bool> DeleteComment(int commentId)
        {
            var comment = _dbContext.Comments.Find(commentId);
            if (comment == null)
            {
                return ServiceResult<bool>.Fail($"Comment with id {commentId} doesn't exist");
            }
            _dbContext.Comments.Remove(comment);
            _dbContext.SaveChanges();
            return ServiceResult<bool>.Ok(true);
        }//done

        public ServiceResult<bool> DeletePostComments(int postId)
        {
            var post = _dbContext.Posts.Include(P => P.Comments).FirstOrDefault(P => P.Id == postId);
            if(post == null)
            {
                return ServiceResult<bool>.Fail($"Post with id {postId} doesn't exist"); 
            }
            _dbContext.Comments.RemoveRange(post.Comments);
            _dbContext.SaveChanges();
            return ServiceResult<bool>.Ok(true);
        }//done

        public ServiceResult<bool> DeleteUserComment(int userId)
        {
            var user = _dbContext.Users.Include(U => U.Comments).FirstOrDefault(U => U.Id == userId);
            if (user == null)
            {
                return ServiceResult<bool>.Fail($"User with id {userId} doesn't exist");
            }
            _dbContext.Comments.RemoveRange(user.Comments);
            _dbContext.SaveChanges();
            return ServiceResult<bool>.Ok(true);
        }//done
       
    }
}
