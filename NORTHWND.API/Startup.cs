using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NORTHWND.API.Middlewares;
using NORTHWND.BLL.Operations;
using NORTHWND.Core.Abstractions;
using NORTHWND.Core.Abstractions.Operations;
using NORTHWND.DAL;

namespace NORTHWND.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<NORTHWNDContext>(x => x.UseSqlServer(Configuration.GetConnectionString("default")));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
             .AddCookie(options => {});
            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddScoped<IProductOperations, ProductOperations>();
            services.AddScoped<IOrderDetailOperations, OrderDetailOperations>();
            services.AddScoped<ICustomerOperations, CustomerOperations>();
            services.AddScoped<IEmployeeOperations, EmployeeOperations>();
            services.AddScoped<IOrderOperations, OrderOperations>();
            services.AddScoped<IUserOperations, UserOperations>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
