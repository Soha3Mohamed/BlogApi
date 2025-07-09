using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using UserPostApi.Models.Data;
using UserPostApi.Models.DTOs.Comment;
using UserPostApi.Models.Entities;
using UserPostApi.Services.Interfaces;

namespace UserPostApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _service;

        public CommentController(ICommentService service)
        {
            this._service = service;
        }
        //this should be only used if we need because it is bad for performance and only for debugging purposes i think
        [HttpGet]
        public IActionResult GetAllComments()
        {
            var result = _service.GetAllComments();
            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [HttpGet("{commentId}")]
        public IActionResult GetComment(int commentId)
        {
            var result = _service.GetComment(commentId);
            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [HttpGet("post/{postId}")] //GET /api/comment/post/5 → gets comments on post 5
        public IActionResult GetPostComments(int postId)
        {
            var result = _service.GetPostComments(postId);
            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }
            return Ok(result.Data);
           
        }

        [HttpGet("user/{userId}")]  //GET /api/comment/user/3 → gets comments by user 3
        public IActionResult GetUserComments(int userId)
        {
            var result = _service.GetUserComments(userId);
            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }
            
            return Ok(result.Data);
          
        }

        [HttpPost("post/{postId}/user/{userId}")] //POST /api/comment/post/7/user/3
        public IActionResult AddComment( AddCommentDTO addCommentDTO, int postId, int userId)
        {

            var result = _service.AddComment(addCommentDTO,postId,userId);

            return Ok(result.Data);
        }
        [HttpPut("{commentId}")]
        public IActionResult UpdateComment(AddCommentDTO addCommentDTO, int commentId)
        {

            var result = _service.UpdateComment(addCommentDTO, commentId);
            return Ok(result.Data);
        }

        [HttpDelete("{commentId}")]
        public IActionResult DeleteComment(int commentId)
        {
            var result = _service.DeleteComment(commentId);
            if(!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }
            
            return Ok(result.Data);
        }

        [HttpDelete("{postId}/post")]
        public IActionResult DeletePostComments(int postId)
        {
            var result = _service.DeleteComment(postId);
            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [HttpDelete("{userId}/user")]
        public IActionResult DeleteUserComments(int userId)
        {
            var result = _service.DeleteComment(userId);
            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
    }
}
/*
 [HttpDelete("users/{id}")]
public IActionResult DeleteUser(int id)
{
    var user = _context.Users
        .Include(u => u.Posts)
        .Include(u => u.Comments)
        .FirstOrDefault(u => u.Id == id);

    if (user == null)
        return NotFound();

    _context.Comments.RemoveRange(user.Comments);
    _context.Posts.RemoveRange(user.Posts);
    _context.Users.Remove(user);

    _context.SaveChanges();

    return NoContent();
}

 */
