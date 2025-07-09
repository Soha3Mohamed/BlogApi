using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserPostApi.Models.Data;
using UserPostApi.Models.DTOs.Post;
using UserPostApi.Models.Entities;
using UserPostApi.Services.Interfaces;

namespace UserPostApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        
        private readonly IPostService _service;

        public PostController(IPostService service)
        {
           
            this._service = service;
        }

        [HttpGet()]
        public IActionResult GetAllPosts()
        {
            var result = _service.GetAllPosts();
            return Ok(result.Data);
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserPosts(int userId)
        {
            var result = _service.GetUserPosts(userId);
            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }
           
            return Ok(result.Data);
        }


        [HttpGet("{UserId}/posts/{PostId}")]

        public IActionResult GetPost(int UserId, int PostId)
        {
            var result = _service.GetPost(UserId, PostId);
            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }
            
            return Ok(result.Data);
        }


        [HttpPost("post/{userId}")]
        public IActionResult AddPost(int userId, AddPostDTO addPostDTO)
        {
            var result = _service.AddPost(addPostDTO, userId);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [HttpPut("{postId}")]
        public IActionResult UpdatePost(int postId, AddPostDTO addPostDTO)
        {
            var result = _service.UpdatePost(addPostDTO, postId); 

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        
        }


        [HttpDelete("{id}")]
        public IActionResult DeletePost(int id)
        {
            var result = _service.DeletePost(id);
            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [HttpDelete("{userId}/User")]
        public IActionResult DeleteUserPosts(int userId)
        {
            var result = _service.DeleteUserPosts(userId);
            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

    }
}
