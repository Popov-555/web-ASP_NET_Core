using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Рабочка_beta_1._0.Models;
using Microsoft.EntityFrameworkCore;

namespace Рабочка_beta_1._0.Controllers
{
    public class AuthController : Controller
    {
        private readonly IjobsContext _context;
        public AuthController(IjobsContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string login, string password)
        {
            var user = _context.Users
                .Include(r=>r.RoleNavigation)
                .FirstOrDefault(u=>u.Login == login && u.Password == password);
            if (user == null)
            {
                ViewBag.Error = "Неверный логин или пароль";
                return View();
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.RoleNavigation.Title)
                
            };
            var identity = new ClaimsIdentity( claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity)
            );

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Register(string Username, string Phone, string Adress, string Login, string Password)
        {
            if(_context.Users.Any(u=>u.Login == Login))
            {
                ViewBag.Error = "Такой логин уже существует";
                return View("Login");
            }
            var user = new User
            {
                Username = Username,
                Login = Login,
                Password = Password,
                Role = 1,
                CreatedAt = DateTime.Now,
                Phone = Phone,
                Address = Adress,
                Avatar = ""
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Login");
        }
    }
}
