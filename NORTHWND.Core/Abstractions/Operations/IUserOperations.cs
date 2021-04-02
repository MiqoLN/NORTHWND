using Microsoft.AspNetCore.Http;
using NORTHWND.Core.BusinessModels;
using System.Threading.Tasks;

namespace NORTHWND.Core.Abstractions.Operations
{
    public interface IUserOperations
    {
        Task Logout(HttpContext context);
        Task Register(RegisterModel model, HttpContext context);
        Task Login(LoginModel model, HttpContext context);
    }
}
