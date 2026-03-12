using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Рабочка_beta_1._0.Models;

namespace Рабочка_beta_1._0.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IjobsContext _context;

        public ProfileController(IjobsContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index() 
        {
            return View("Index");
        }
    }
}
