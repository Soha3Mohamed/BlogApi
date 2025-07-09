using Microsoft.EntityFrameworkCore;
using UserPostApi.Helpers;
using UserPostApi.Models.Data;
using UserPostApi.Models.DTOs.Post;
using UserPostApi.Models.DTOs.User;
using UserPostApi.Models.Entities;
using UserPostApi.Services.Interfaces;

namespace UserPostApi.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        } 

        public ServiceResult<List<GetUserDTO>> GetAllUsers()
        {
            var allUsers = _dbContext.Users.
               Include(U => U.Posts)
               .Select(U => new GetUserDTO
               {
                   Id = U.Id,
                   Name = U.Name,
                   Email = U.Email,
                   Posts = U.Posts.Select(P => new GetPostDTO
                   {
                       Title = P.Title,
                       Description = P.Description,
                   }).ToList()
               }).ToList();
            if(allUsers.Count <= 0)
            {
                return ServiceResult<List<GetUserDTO>>.Fail("No users Found..");
            }
            return ServiceResult<List<GetUserDTO>>.Ok(allUsers);
        }

        public ServiceResult<GetUserDTO> GetUser(int id)
        {
            var user = _dbContext.Users.Include(U => U.Posts).FirstOrDefault(U => U.Id == id);
            if (user == null)
            {
                return ServiceResult<GetUserDTO>.Fail($"No user was found with this Id: {id}..");
            }
            var getUserDTO = new GetUserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Posts = user.Posts.Select(P => new GetPostDTO
                {
                    Title = P.Title,
                    Description = P.Description,
                }).ToList()
            }; 
            return ServiceResult<GetUserDTO>.Ok(getUserDTO);
        }
       
        public ServiceResult<GetUserDTO> AddUser(AddUserDTO addUserDTO)
        {
            var user = new User
            {
                
                Name = addUserDTO.Name,
                Email = addUserDTO.Email,
                Password = addUserDTO.Password
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            var getUserDTO = new GetUserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Posts = user.Posts.Select(P => new GetPostDTO
                {
                    Title = P.Title,
                    Description = P.Description,
                }).ToList()
            };
            return ServiceResult<GetUserDTO>.Ok(getUserDTO);
        }
        public ServiceResult<GetUserDTO> UpdateUser(AddUserDTO addUserDTO, int userId)
        {
            var user = _dbContext.Users.Find(userId);

            if (user == null)
            {
                return ServiceResult<GetUserDTO>.Fail($"No user with this Id: {userId} was found..");
            }

            user.Name = addUserDTO.Name;
            user.Email = addUserDTO.Email;
            user.Password = addUserDTO.Password;
            _dbContext.SaveChanges();

            var getUserDTO = new GetUserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Posts = user.Posts.Select(P => new GetPostDTO
                {
                    Title = P.Title,
                    Description = P.Description,
                }).ToList()
            };
            return ServiceResult<GetUserDTO>.Ok(getUserDTO);
        }
        public ServiceResult<bool> DeleteUser(int id)
        {
            var user = _dbContext.Users
                      .Include(u => u.Posts)
                      .Include(u => u.Comments)
                      .FirstOrDefault(u => u.Id == id);

            if (user == null)
                return ServiceResult<bool>.Fail($"No user was found with this Id: {id}");

            _dbContext.Comments.RemoveRange(user.Comments);
            _dbContext.Posts.RemoveRange(user.Posts);
            _dbContext.Users.Remove(user);

            _dbContext.SaveChanges();
            return ServiceResult<bool>.Ok(true);
        }

        

       

       
    }
}
