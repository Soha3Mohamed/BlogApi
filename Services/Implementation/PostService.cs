using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using UserPostApi.Helpers;
using UserPostApi.Models.Data;
using UserPostApi.Models.DTOs.Comment;
using UserPostApi.Models.DTOs.Post;
using UserPostApi.Models.Entities;
using UserPostApi.Services.Interfaces;

namespace UserPostApi.Services.Implementation
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _dbContext;

        public PostService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public ServiceResult<List<GetPostDTO>> GetAllPosts()
        {
            var allPosts = _dbContext.Posts.Include(P => P.Comments).Select(P => new GetPostDTO
            {
                Id = P.Id,
                Title = P.Title,
                Description = P.Description,
                CommentCount = P.Comments.Count
            }).ToList();
            return ServiceResult<List<GetPostDTO>>.Ok(allPosts);
        }

        public ServiceResult<GetPostDTO> GetPost(int postId, int userId)
        {
            var post = _dbContext.Posts
                       .FirstOrDefault(p => p.Id == postId && p.UserId == userId);
            if (post == null)
            {
                return ServiceResult<GetPostDTO>.Fail($"Post with post id: {postId} and user id: {userId}doesn't exist");
            }
            var postDTO = new GetPostDTO
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                CommentCount = post.Comments.Count(),
            };
            return ServiceResult<GetPostDTO>.Ok(postDTO);
        }

        public ServiceResult<List<GetPostDTO>> GetUserPosts(int userId)
        {
            var user = _dbContext.Users
                      .Include(u => u.Posts)
                      .FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return ServiceResult<List<GetPostDTO>>.Fail($"User with Id: {userId} is not found");
            }
            var userPosts = user.Posts.Select(P => new GetPostDTO
            {
                Id = P.Id,
                Title = P.Title,
                Description = P.Description,
                CommentCount = P.Comments.Count()
            }).ToList();

            return ServiceResult< List<GetPostDTO>>.Ok(userPosts);
        }
        public ServiceResult<GetPostDTO> AddPost(AddPostDTO addPostDTO, int userId)
        {
            var user = _dbContext.Users.Find(userId);
            if (user == null)
            {
                return ServiceResult<GetPostDTO>.Fail($"User with id: {userId} is not found");
            }
            var post = new Post
            {
                Title = addPostDTO.Title,
                Description = addPostDTO.Description,
                UserId = userId
            };
            _dbContext.Posts.Add(post);
            _dbContext.SaveChanges();
            var postDTO = new GetPostDTO
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                CommentCount = post.Comments.Count()
            };
            return ServiceResult<GetPostDTO>.Ok(postDTO);
        }

        public ServiceResult<GetPostDTO> UpdatePost(AddPostDTO addPostDTO, int postId)
        {
            var post = _dbContext.Posts.Find(postId);
            if (post == null)
            {
                return ServiceResult<GetPostDTO>.Fail($"Post with id: {postId} is not found"); 
            }
            post.Title = addPostDTO.Title;
            post.Description = addPostDTO.Description;
            _dbContext.SaveChanges();

            var postDTO = new GetPostDTO
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                CommentCount = post.Comments.Count()
            };
            return ServiceResult<GetPostDTO>.Ok(postDTO); ;
        }
        public ServiceResult<bool> DeletePost(int id)
        {
            var post = _dbContext.Posts.Find(id);
            if (post == null)
            {
                return ServiceResult<bool>.Fail($"Post with id: {id} is not found"); 
            }
            _dbContext.Posts.Remove(post);
            _dbContext.SaveChanges();
            return ServiceResult<bool>.Ok(true);
        }

        public ServiceResult<bool> DeleteUserPosts(int userId)
        {
            var user = _dbContext.Users.Include(u => u.Posts).FirstOrDefault(u => u.Id == userId);
            if (user == null )
            {
                return ServiceResult<bool>.Fail($"User with id: {userId} is not found");
            }
            if (user.Posts.Count == 0)
            {
                return ServiceResult<bool>.Fail($"User with id {userId}has no posts");
            }

            _dbContext.Posts.RemoveRange(user.Posts);
            _dbContext.SaveChanges();
            return ServiceResult<bool>.Ok(true); 
        }
    }
}
