using LibraryV4.Domain.Interfaces;
using LibraryV4.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryV4.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUser()
        {
            var users = _userService.GetAllUser();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<string> AddUser(User user)
        {
            var result = _userService.AddUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, result);
        }

        [HttpPut("{id}")]
        public ActionResult<string> UpdateUser(int id, User user)
        {

            if (id != user.Id)
            {
                return BadRequest("ID User tidak cocok");
            }

            var existingUser = _userService.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound("User tidak ditemukan");
            }

            var result = _userService.UpdateUser(user);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<string> DeleteUser(int id)
        {
            var result = _userService.DeleteUser(id);
            if (result.Contains("User tidak ditemukan"))
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
