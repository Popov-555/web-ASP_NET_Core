using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Рабочка_beta_1._0.Models;

namespace Рабочка_beta_1._0.Controllers
{
    public class DetailInfoController : Controller
    {
        private readonly IjobsContext _context;
        public DetailInfoController(IjobsContext context)
        {
            _context = context;
        }
        public IActionResult Details(int id)
        {   
            var job = _context.Jobs
                .Include(y => y.Category)
                .Include(e => e.Employer)
                .FirstOrDefault(j => j.Id == id);
            if (job == null)
                return NotFound();
            return View(job);
        }
    }
}
