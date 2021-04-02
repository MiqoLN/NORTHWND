using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NORTHWND.Core.Abstractions.Operations;
using NORTHWND.Core.BusinessModels;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NORTHWND.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserOperations _userOperations;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsersController(IUserOperations userOperations, IHttpContextAccessor httpContextAccessor)
        {
            _userOperations = userOperations;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                await _userOperations.Register(model, HttpContext);
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
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
        [Authorize]
        [HttpGet("info")]
        public IActionResult GetInfo()
        {
            var user = HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;
            return Ok(user);
        }
    }
}
