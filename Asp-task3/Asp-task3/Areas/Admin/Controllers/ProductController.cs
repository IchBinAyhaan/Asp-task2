using Asp_task3.Areas.Admin.Models.Product;
using Asp_task3.Areas.Admin.Models.Shop;
using Asp_task3.Data;
using Asp_task3.Entities;
using Asp_task3.Utilities.File;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Asp_task3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFileService _fileService;

        public ProductController(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        #region List
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new ProductIndexVM
            {
                Products = await _context.Products.ToListAsync()
            };

            return View(model);
        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new ProductCreateVM
            {
                ShopCategories = await _context.ShopCategories
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM model, IFormFile photo)
        {
            if (!ModelState.IsValid)
            {
                model.ShopCategories = await _context.ShopCategories
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToListAsync();
                return View(model);
            }

            var existingProduct = await _context.Products
                .FirstOrDefaultAsync(p => p.Title.ToLower() == model.Title.ToLower());
            if (existingProduct != null)
            {
                ModelState.AddModelError("Title", "A product with this title already exists");
                model.ShopCategories = await _context.ShopCategories
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToListAsync();
                return View(model);
            }

            // Handle file upload
            string photoPath = string.Empty;
            if (photo != null && photo.Length > 0)
            {
                photoPath = await _fileService.Upload(photo, "product_images");
            }

            var product = new Product
            {
                Title = model.Title,
                Size = model.Size,
                Price = model.Price,
                PhotoPath = photoPath,
                ShopCategoryId = model.ShopCategoryId
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            var model = new ProductUpdateVM
            {
                Title = product.Title,
                Size = product.Size,
                Price = product.Price,
                PhotoPath = product.PhotoPath,
                ShopCategoryId = product.ShopCategoryId,
                ShopCategories = await _context.ShopCategories
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, ProductUpdateVM model, IFormFile photo)
        {
            if (!ModelState.IsValid)
            {
                model.ShopCategories = await _context.ShopCategories
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToListAsync();
                return View(model);
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            var isExist = await _context.Products
                .AnyAsync(p => p.Title.ToLower() == model.Title.ToLower() && p.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("Title", "A product with this title already exists");
                return View(model);
            }

            // Handle file upload
            if (photo != null && photo.Length > 0)
            {
                // Delete old image if a new one is uploaded
                if (!string.IsNullOrEmpty(product.PhotoPath))
                {
                    _fileService.Delete("product_images", product.PhotoPath);
                }

                product.PhotoPath = await _fileService.Upload(photo, "product_images");
            }

            product.Title = model.Title;
            product.Size = model.Size;
            product.Price = model.Price;
            product.ShopCategoryId = model.ShopCategoryId;
            product.ModifiedAt = DateTime.Now;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            // Delete associated photo if exists
            if (!string.IsNullOrEmpty(product.PhotoPath))
            {
                _fileService.Delete("product_images", product.PhotoPath);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}