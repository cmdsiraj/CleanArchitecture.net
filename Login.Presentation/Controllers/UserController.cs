using Login.Application.Dtos;
using Login.Application.IServices;
using Login.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Login.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }
        [HttpPost("/signup")]
        public ActionResult<User> SignUp([FromBody] SignUpDTO signupDto)
        {
            if(signupDto == null)
            {
                ModelState.AddModelError("null", "Entries must not be null");
                return BadRequest(ModelState);
            }
            User? result = _userService.CreateUser(signupDto);
            if(result == null)
            {
                ModelState.AddModelError("Error", "Error while registering the user");
                return BadRequest(ModelState);
            }
            return Ok(result);
        }

        [HttpPost("/login")]
        public ActionResult<User> Login([FromBody] LoginDTO loginDto)
        {
            if(loginDto == null)
            {
                ModelState.AddModelError("null", "Entries must not be null");
                return BadRequest(ModelState);
            }
            TokenDTO? result = _userService.Login(loginDto);
            if(result == null)
            {
                ModelState.AddModelError("Error", "Unable to login");
                return BadRequest(ModelState);
            }
            return Ok(result);
        }

        [HttpGet("/users/all")]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }
    }
}
