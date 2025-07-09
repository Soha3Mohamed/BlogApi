using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserPostApi.Models.DTOs.Category;
using UserPostApi.Services.Interfaces;

namespace UserPostApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            this._service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCategoryDTO))]
        public ActionResult<List<GetCategoryDTO>> GetAllCategories()
        {
            var result = _service.GetAllCategories();
            return Ok(result);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCategoryDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategory(int categoryId)
        {
            var result = _service.GetCategory(categoryId);
            if (result == null)
            {
                return NotFound();
            }
                
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult AddCategory(AddCategoryDTO addCategoryDTO)
        {
            var result = _service.AddCategory(addCategoryDTO);
            return CreatedAtAction(nameof(GetCategory), new {categoryId = result.Id}, result);
        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCategoryDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateCategory(AddCategoryDTO addCategoryDTO ,int categoryId)
        {
            var result = _service.UpdateCategory(addCategoryDTO,categoryId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{categoryId}")]
        public IActionResult DeleteCategory(int categoryId)
        {
            var result  = _service.DeleteCategory(categoryId);
            return Ok(result);
        }

        [HttpDelete("{categoryId}/posts")]
        public IActionResult DeleteCategoryPosts(int categoryId)
        {
            var result = _service.DeleteCategoryPosts(categoryId);
            return Ok(result);
        }
    }
}
