using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserLogin_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _UserRepository;
        public UserController(  IUserRepository userRepository)
        {
            _UserRepository = userRepository;

        }
        [HttpPost("Login")]
        public IActionResult Login(string username, string password)
        {
            var user = _UserRepository.GetUserByCredentials(username, password);

            if (user != null)
            {
                // Authentication successful, implement your logic
                return Ok("Successfully login"); // Return to login view with error
            }
            else
            {
                // Authentication failed
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return Ok("invalid username or password"); // Return to login view with error
            }
        }

        [HttpGet("UserDetails")]
        public IActionResult UserDetails()
        {
            var user = _UserRepository.GetUserDetails();

            if (user != null)
            {
                // Authentication successful, implement your logic
                return Ok(user); // Return to login view with error
            }
            else
            {
                // Authentication failed
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return Ok(); // Return to login view with error
            }
        }
        
        [HttpPost("Register")]
        public IActionResult Register(UserRegister model)
        {
            // Call the repository method to register the user
            bool isRegistered = _UserRepository.RegisterUser(model);

            if (isRegistered==false)
            {
                // User registration successful
                return Ok("User registration successful.");
            }
            else
            {
                // User registration failed
                return BadRequest("Failed to register user.");
            }
        }
    }
}
