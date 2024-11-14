using System.ComponentModel.DataAnnotations;

namespace Asp_task3.Areas.Admin.Models.ShopCategory
{
    public class ShopCategoryUpdateVM
    {
        [Required(ErrorMessage = "Please enter name")]
        [MinLength(3, ErrorMessage = "Please enter minimum 3 character")]
        public string Name { get; set; }
    }
}
