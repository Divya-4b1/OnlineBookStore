using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Interfaces;
using OnlineBookStore.Models.DTOs;

namespace OnlineBookStore.Controllers
{
    
        [Route("api/[controller]")]
        [ApiController]
        [EnableCors("reactApp")]
    public class UserController : ControllerBase
        {
            private readonly IUserService _userService;
            
            public UserController(IUserService userService)
            {
                _userService = userService;
            }


            [HttpPost]
        public ActionResult Register(UserDTO ViewModel)
        {
            string message = "";
            try
            {
                var user = _userService.Register(ViewModel);
                if (user != null)
                {
                    return Ok(user);
                }
            }
            catch (DbUpdateException exp)
            {
                message = "Duplicate username";
            }
            catch (Exception)
            {

            }


            return BadRequest(message);
        }

        [HttpPost]
            [Route("Login")]
            public ActionResult Login(UserDTO userDTO)
            {
                var result = _userService.Login(userDTO);
                if (result != null)
                {
                    return Ok(result);
                }
                return Unauthorized("Invalid username or password");
            }
        }
 }

