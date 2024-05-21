using ecommerce.core.Entities;
using ecommerce.core.ISqlRepositories;
using ecommerce.core.Responses;
using MediatR;
using System.Linq;
using System.Linq.Expressions;

namespace ecommerce.application.Product.Queries
{
    public record ProductSearchByTextQuery : IRequest<ICollection<ProductResponse>>
    {
        public string SearchText { get; set; }
    }
    public class ProductSearchByTextHandler(IProductRepository productRepository) : IRequestHandler<ProductSearchByTextQuery, ICollection<ProductResponse>>
    {
        private readonly IProductRepository _productRepository = productRepository;
        public async Task<ICollection<ProductResponse>> Handle(ProductSearchByTextQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.Get<ProductResponse>(
                d => d.Name.Contains(request.SearchText)
                || d.Category.Name.Contains(request.SearchText)
                , ProductGetAllQuerySelect);
            return productList;
        }

        private static readonly Expression<Func<Product<Guid>, ProductResponse>> ProductGetAllQuerySelect = p => new ProductResponse
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Thumbnail = p.Thumbnail,
            Rating = 5,
            Category = p.Category.Name,
            Stock = new Random().Next(0, 10),
            InventoryStatus = new Random().Next(0, 10) > 5 ? "INSTOCK" : "LOWSTOCK"

        };
    }
}
