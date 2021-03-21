using Microsoft.AspNetCore.Mvc;
using NORTHWND.Core.Abstractions.Operations;
using NORTHWND.Core.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NORTHWND.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserOperations _userOperations;
        public UsersController(IUserOperations userOperations)
        {
            _userOperations = userOperations;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                await _userOperations.Register(model,HttpContext);
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if(ModelState.IsValid)
            {
                await _userOperations.Login(model, HttpContext);
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _userOperations.Logout(HttpContext);
            return Ok();
        }
    }
}
