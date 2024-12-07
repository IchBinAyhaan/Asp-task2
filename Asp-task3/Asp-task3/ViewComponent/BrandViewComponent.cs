using Asp_task3.Data;
using Asp_task3.Models.Brand;
using Microsoft.AspNetCore.Mvc;

namespace Asp_task3.ViewComponent
{
    public class BrandViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public BrandViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var brands = _context.Brands.ToList();  
            return View(brands);
        }
    }
}
