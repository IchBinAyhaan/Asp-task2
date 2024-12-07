namespace Asp_task3.Entities
{
    public class ShopCategory : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
