using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using NORTHWND.Core.Abstractions;
using NORTHWND.Core.Abstractions.Operations;
using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Entities;
using NORTHWND.Core.Enum;
using NORTHWND.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NORTHWND.BLL.Operations
{
    public class UserOperations : IUserOperations
    {
        private readonly IRepositoryManager _repositories;


        public UserOperations(IRepositoryManager repositories)
        {
            _repositories = repositories;
        }
        public async Task Login(LoginModel model, HttpContext context)
        {
            var user = _repositories.Users.GetSingle(u => u.Email == model.Email && u.Password == model.Password && u.Role == Role.User)
                ?? throw new LogicException("Wrong username or password");
            await Authenticate(user, context);
        }

        public async Task Logout(HttpContext context)
        {
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task Register(RegisterModel model, HttpContext context)
        {
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
        }
        public async Task Authenticate(User user, HttpContext context)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType,user.Role.ToString())
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

        }
    }
}
