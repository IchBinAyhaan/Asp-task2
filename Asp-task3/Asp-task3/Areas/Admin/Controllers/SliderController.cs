using Asp_task3.Areas.Admin.Models.Slider;
using Asp_task3.Data;
using Asp_task3.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Asp_task3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;

        public SliderController(AppDbContext context)
        {
            _context = context;
        }

        #region List

        [HttpGet]
        public IActionResult Index()
        {
            var model = new SliderIndexVM
            {
                Sliders = _context.Sliders.ToList()
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
        public IActionResult Create(SliderCreateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            // Check if a slider with the same title already exists
            var slider = _context.Sliders.FirstOrDefault(s => s.Title.ToLower() == model.Title.ToLower());
            if (slider is not null)
            {
                ModelState.AddModelError("Title", "A slider with this title already exists.");
                return View(model);
            }

            // Create new slider
            slider = new Slider
            {
                Name = model.Name,
                Title = model.Title,
                Description = model.Description,
                PhotoPath = model.PhotoPath
            };

            _context.Sliders.Add(slider);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Update

        [HttpGet]
        public IActionResult Update(int id)
        {
            var slider = _context.Sliders.Find(id);
            if (slider is null) return NotFound();

            var model = new SliderUpdateVM
            {
                Name = slider.Name,
                Title = slider.Title,
                Description = slider.Description,
                PhotoPath = slider.PhotoPath
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(int id, SliderUpdateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var slider = _context.Sliders.Find(id);
            if (slider is null) return NotFound();

            // Check if another slider with the same title exists (excluding the current one)
            var isExist = _context.Sliders.Any(s => s.Title.ToLower() == model.Title.ToLower() && s.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("Title", "A slider with this title already exists.");
                return View(model);
            }

            slider.Name = model.Name;
            slider.Title = model.Title;
            slider.Description = model.Description;
            slider.PhotoPath = model.PhotoPath;
            slider.ModifiedAt = DateTime.Now;

            _context.Sliders.Update(slider);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var slider = _context.Sliders.Find(id);
            if (slider is null) return NotFound();

            _context.Sliders.Remove(slider);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}