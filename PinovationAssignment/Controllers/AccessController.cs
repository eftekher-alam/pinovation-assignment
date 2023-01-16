using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PinovationAssignment.Data;
using PinovationAssignment.Models;
using System.Security.Claims;

namespace PinovationAssignment.Controllers
{
    public class AccessController : Controller
    {

        private readonly IApplicationDbContext _context;

        public AccessController(IApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            ClaimsPrincipal claims = HttpContext.User;
            if (claims.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LogIn logIn)
        {
            if (ModelState.IsValid)
            {
                if (logIn.Email == "admin@admin.com" && logIn.Password == "@123@")
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, logIn.Email),
                        new Claim(ClaimTypes.Role, "Admin"),
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true
                    };


                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);


                    return RedirectToAction("Index", "User");
                }


                TblUsers user = _context.GetUserByEmail(logIn.Email);
                if (user == null)
                {
                    ModelState.AddModelError("Email", "Email is incorrect.");
                    return View(logIn);
                }
                else if (user.password == logIn.Password)
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, logIn.Email),
                        new Claim(ClaimTypes.Role, "User"),
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true
                    };


                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

                    return RedirectToAction("Details", "User", new { userId = user.userId });
                }
                else
                {
                    ModelState.AddModelError("Password", "Password is incorrect.");
                    return View(logIn);
                }
            }
            return View(logIn);
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }
    }
}
