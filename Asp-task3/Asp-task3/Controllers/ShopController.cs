using Asp_task3.Data;
using Asp_task3.Models.Shop;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp_task3.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new ShopIndexVM
            {
                ShopCategories = _context.ShopCategories.Include(sc => sc.Products).ToList(),
                Products = _context.Products.Include(p => p.ShopCategory).ToList()
                                    

            };

            return View(model);
        }

        public IActionResult Details(int id)
        {
            return View();
        }
    }
}
