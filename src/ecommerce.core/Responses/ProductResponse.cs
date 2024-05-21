
namespace ecommerce.core.Responses
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Thumbnail { get; set; }
        public int Rating { get; set; }
        public string Category { get; set; }
        public int Stock { get; set; }
        public string InventoryStatus { get; set; }
    }
}
