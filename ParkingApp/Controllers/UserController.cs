using Microsoft.AspNetCore.Mvc;
using ParkingApp.Models.DTOs;
using ParkingApp.Services;

namespace ParkingApp.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("")]
        public IActionResult Register(RegisterDTO registerDTO)
        {
            bool sucess=userService.Register(registerDTO).Result;
            if (sucess)
            {
                return Ok(null);
            }
            else 
            {
                return BadRequest();
            }
        }

        [HttpPost("")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            string token=userService.Login(loginDTO).Result;
            if (token == null)
            {
                return BadRequest();
            }
            else
            {
                Response.Cookies.Append("jwt", token, new CookieOptions
                {
                    HttpOnly = true
                });
                return Ok(null);
            }
        }
    }
}
