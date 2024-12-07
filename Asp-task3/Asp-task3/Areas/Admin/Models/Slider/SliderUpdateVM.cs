using System.ComponentModel.DataAnnotations;

namespace Asp_task3.Areas.Admin.Models.Slider
{
    public class SliderUpdateVM
    {
        [Required(ErrorMessage = "Please enter the slider name.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The name must be between 3 and 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the slider title.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "The title must be between 3 and 200 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please provide a photo path.")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "The photo path must be between 5 and 255 characters.")]
        public string PhotoPath { get; set; }

        [StringLength(1000, ErrorMessage = "Description must not exceed 1000 characters.")]
        public string Description { get; set; }
    }
}
