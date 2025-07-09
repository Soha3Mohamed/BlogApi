using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserPostApi.Models.Data;
using UserPostApi.Models.DTOs.Post;
using UserPostApi.Models.DTOs.User;
using UserPostApi.Models.Entities;
using UserPostApi.Services.Interfaces;

namespace UserPostApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            this._service = service;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
           // var allUsers = dbContext.Users.ToList(); //doesn't get posts related to user
           var result =_service.GetAllUsers();
            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var result = _service.GetUser(id);
            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult AddUser(AddUserDTO addUserDTO)
        {
            var result = _service.AddUser(addUserDTO);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return CreatedAtAction(nameof(GetUser),new { id = result.Data.Id } ,result); // Assuming GetUser returns the user with the given ID
            //return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, AddUserDTO addUserDTO)
        {
            var result = _service.UpdateUser(addUserDTO, id); 
            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var result =  _service.DeleteUser(id);
            if (!result.Success)
            {
                return NotFound(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

       
    }
}
