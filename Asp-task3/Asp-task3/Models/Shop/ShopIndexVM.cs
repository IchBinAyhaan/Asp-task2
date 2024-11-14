using Asp_task3.Entities;

namespace Asp_task3.Models.Shop
{
    public class ShopIndexVM
    {
        public List<ShopCategory> ShopCategories { get; set; }
        public List<Product> Products { get; set; }
    }
}
