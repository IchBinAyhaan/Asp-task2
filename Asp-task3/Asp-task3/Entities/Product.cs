namespace Asp_task3.Entities
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }
        public string Size { get; set; }
        public string Price { get; set; }
        public string PhotoPath { get; set; }
        public int ShopCategoryId { get; set; }
        public ShopCategory ShopCategory { get; set; }
        
    }
}
