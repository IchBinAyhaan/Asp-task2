using Asp_task3.Data;
using Asp_task3.Models.Home;
using Asp_task3.Models.Shop;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp_task3.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new HomeIndexVM
            {
                Sliders= _context.Sliders.ToList()
                


            };

            return View(model);
        }
    }
}
