using Microsoft.EntityFrameworkCore;
using UserPostApi.Models.Data;
using UserPostApi.Models.DTOs;
using UserPostApi.Models.DTOs.Category;
using UserPostApi.Models.DTOs.Post;
using UserPostApi.Models.Entities;
using UserPostApi.Services.Interfaces;

namespace UserPostApi.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public GetCategoryDTO AddCategory(AddCategoryDTO addCategoryDTO)
        {
            var category = new Category
            {
                Name = addCategoryDTO.Name,
                Description = addCategoryDTO.Description,

            };
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            var getCategoryDTO = new GetCategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Posts = category.Posts.Select(P => new GetPostDTO
                {
                    Title = P.Title,
                    Description = P.Description,
                }).ToList()
            };
            return getCategoryDTO;

        }

        public bool DeleteCategory(int id)
        {
            var category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
              return false;
            }
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            return true;
        }

        public bool DeleteCategoryPosts(int categoryId)
        {
            var category = _dbContext.Categories.Include(C=>C.Posts).FirstOrDefault(C=>C.Id == categoryId);
            if (category == null || category.Posts.Count <= 0)
            {
                return false;
            }

            // category.Posts.RemoveAll(c => c.Id == categoryId);
            _dbContext.Posts.RemoveRange(category.Posts);
            _dbContext.SaveChanges();
            return true;

        }

        public List<GetCategoryDTO> GetAllCategories()
        {
            var allCategories = _dbContext.Categories.Include(C => C.Posts).Select(C => new GetCategoryDTO
            {
                Id = C.Id,
                Name = C.Name,
                Description = C.Description,
                Posts = C.Posts.Select(P => new GetPostDTO
                {
                    Title = P.Title,
                    Description = P.Description,
                }).ToList()
            }).ToList();
            return allCategories;
        }

        public GetCategoryDTO GetCategory(int id)
        {
            var  category = _dbContext.Categories.Include(C=>C.Posts).FirstOrDefault(C=>C.Id == id);
            if (category == null)
            {
                return null;
            }
            var getCategoryDTO = new GetCategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Posts = category.Posts.Select(P => new GetPostDTO
                {
                    Title = P.Title,
                    Description = P.Description,
                }).ToList()
            };
            return getCategoryDTO;
        }

        public List<GetPostDTO> GetPostsByCategoryId(int categoryId)
        {
            var category = _dbContext.Categories.Include(C => C.Posts).FirstOrDefault(C => C.Id == categoryId);

            if(category == null || category.Posts.Count <= 0)
            {
                return null;
            }
            var postsDTO = category.Posts.Select(P => new GetPostDTO
            {
                Title = P.Title,
                Description = P.Description,
                CommentCount = P.Comments.Count
            }).ToList();
            return postsDTO;
        }

        public GetCategoryDTO UpdateCategory(AddCategoryDTO addCategoryDTO, int categoryId)
        {
            var category = _dbContext.Categories.Include(C => C.Posts).FirstOrDefault(C => C.Id == categoryId);
            //there is mislogic in here
            if (category == null)
            {
                return null;
            }
            category.Name = addCategoryDTO.Name;
            category.Description = addCategoryDTO.Description;

            _dbContext.SaveChanges();
            var getCategoryDTO = new GetCategoryDTO()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Posts = category.Posts.Select(P => new GetPostDTO
                {
                    Title = P.Title,
                    Description = P.Description,
                }).ToList()
            };
            return getCategoryDTO;

        }
    }
}
