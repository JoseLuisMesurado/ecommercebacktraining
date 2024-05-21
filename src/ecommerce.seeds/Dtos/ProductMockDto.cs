namespace ecommerce.seeds.Dtos
{
    public class ProductList
    {
        public List<ProductMockDto> Products { get; set; }
    }
    public class ProductMockDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Thumbnail { get; set;}

        public List<string>  Images { get; set; }

    }

    
}
