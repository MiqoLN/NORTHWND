using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NORTHWND.Core.Abstractions;
using NORTHWND.Core.Abstractions.Operations;
using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Entities;
using NORTHWND.Core.Enum;
using NORTHWND.Core.Exceptions;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NORTHWND.BLL.Operations
{
    public class UserOperations : IUserOperations
    {
        private readonly IRepositoryManager _repositories;
        private readonly ILogger<UserOperations> _logger;

        public UserOperations(IRepositoryManager repositories, ILogger<UserOperations> logger)
        {
            _repositories = repositories;
            _logger = logger;
        }
        public async Task Login(LoginModel model, HttpContext context)
        {
            _logger.LogInformation("Login started");
            var user = _repositories.Users.GetSingle(u => u.Email == model.Email && u.Password == model.Password && u.Role == Role.User)
                ?? throw new LogicException("Wrong username or password");
            await Authenticate(user, context);
            _logger.LogInformation("Login finished");
        }

        public async Task Logout(HttpContext context)
        {
            _logger.LogInformation("Logout started");
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _logger.LogInformation("Logout finished");
        }

        public async Task Register(RegisterModel model, HttpContext context)
        {
            _logger.LogInformation("Register started");
            User user = _repositories.Users.GetSingle(u => u.Email == model.Email);
            if (user == null)
            {
                user = new User
                {
                    Role = Role.User,
                    Email = model.Email,
                    Password = model.Password
                };
                _repositories.Users.Add(user);
                await _repositories.SaveChangesAsync();
                await Authenticate(user, context);
            }
            else
                throw new LogicException("User already exists");
            _logger.LogInformation("Register finished");
        }
        public async Task Authenticate(User user, HttpContext context)
        {
            _logger.LogInformation("Authenticate started");
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType,user.Role.ToString())
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            _logger.LogInformation("Authenticate finished");
        }
    }
}
