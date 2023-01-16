using Microsoft.AspNetCore.Authentication.Cookies;
using PinovationAssignment.Data;

namespace PinovationAssignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<IApplicationDbContext, ApplicationDbContext>();
            builder.Services.AddSingleton<IUtility, Utility>();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie( option =>
                {
                    option.LoginPath = "/Access/Login";
                    option.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Access}/{action=Login}/{id?}");

            app.Run();
        }
    }
}