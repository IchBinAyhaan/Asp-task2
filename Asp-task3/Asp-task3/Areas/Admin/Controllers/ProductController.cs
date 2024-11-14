using Asp_task3.Areas.Admin.Models.Product;
using Asp_task3.Areas.Admin.Models.Shop;
using Asp_task3.Data;
using Asp_task3.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Asp_task3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        #region List

        [HttpGet]
        public IActionResult Index()
        {
            var model = new ProductIndexVM
            {
                // Kategorilerle bağımsız sadece ürünleri alıyoruz
                Products = _context.Products.ToList()
            };

            return View(model);
        }

        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            var model = new ProductCreateVM
            {
                // Kategorilerle ilgili bir alan bulunmuyor
                // Categories listesini boş bırakıyoruz
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ProductCreateVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existingProduct = _context.Products.FirstOrDefault(p => p.Title.ToLower() == model.Title.ToLower());
            if (existingProduct != null)
            {
                ModelState.AddModelError("Title", "Bu adda məhsul mövcuddur");
                return View(model);
            }

            var product = new Product
            {
                Title = model.Title,
                Size = model.Size,
                Price = model.Price,
                PhotoPath = model.PhotoPath,
                // Kategoriyi kaldırdık, ShopCategoryId'yi eklemiyoruz
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Update

        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            var model = new ProductUpdateVM
            {
                Title = product.Title,
                Size = product.Size,
                Price = product.Price,
                PhotoPath = product.PhotoPath,
                // Kategori ile ilgili herhangi bir veri yok
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(int id, ProductUpdateVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            var isExist = _context.Products.Any(p => p.Title.ToLower() == model.Title.ToLower() && p.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("Title", "Bu adda məhsul mövcuddur");
                return View(model);
            }

            product.Title = model.Title;
            product.Size = model.Size;
            product.Price = model.Price;
            product.PhotoPath = model.PhotoPath;
            product.ModifiedAt = DateTime.Now;

            _context.Products.Update(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
