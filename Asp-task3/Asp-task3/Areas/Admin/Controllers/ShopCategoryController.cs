using Asp_task3.Areas.Admin.Models.ShopCategory;
using Asp_task3.Data;
using Asp_task3.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Asp_task3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ShopCategoryController : Controller
    {
        private readonly AppDbContext _context;

        public ShopCategoryController(AppDbContext context)
        {
            _context = context;
        }
        #region List

        [HttpGet]
        public IActionResult Index()
        {
            var model = new ShopCategoryIndexVM
            {
                ShopCategories = _context.ShopCategories.ToList()
            };

            return View(model);
        }

        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ShopCategoryCreateVM model)
        {
            if (!ModelState.IsValid) return View();

            var shopCategory = _context.ShopCategories.FirstOrDefault(sc => sc.Name.ToLower() == model.Name.ToLower());
            if (shopCategory is not null)
            {
                ModelState.AddModelError("Name", "Bu adda kateqoriya mövcuddur");
                return View();
            }

            shopCategory = new ShopCategory
            {
                Name = model.Name
            };

            _context.ShopCategories.Add(shopCategory);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Update

        [HttpGet]
        public IActionResult Update(int id)
        {
            var shopCategory = _context.ShopCategories.Find(id);
            if (shopCategory is null) return NotFound();

            var model = new ShopCategoryUpdateVM
            {
                Name = shopCategory.Name
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(int id, ShopCategoryUpdateVM model)
        {
            if (!ModelState.IsValid) return View();

            var shopCategory = _context.ShopCategories.Find(id);
            if (shopCategory is null) return NotFound();

            var isExist = _context.ShopCategories.Any(sc => sc.Name.ToLower() == model.Name.ToLower() && sc.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu adda kateqoriya mövcuddur");
                return View();
            }

            if (shopCategory.Name != model.Name)
                shopCategory.ModifiedAt = DateTime.Now;

            shopCategory.Name = model.Name;

            _context.ShopCategories.Update(shopCategory);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var shopCategory = _context.ShopCategories.Find(id);
            if (shopCategory is null) return NotFound();

            _context.ShopCategories.Remove(shopCategory);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
