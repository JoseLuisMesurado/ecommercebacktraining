using ecommerce.core.Entities;
using ecommerce.core.ISqlRepositories;
using ecommerce.core.Responses;
using MediatR;
using System.Linq.Expressions;

namespace ecommerce.application.Category.Queries
{
    public class CategoryGetAllQuery : IRequest<ICollection<CategoryResponse>>;
    public class CategoryGetAllHandler(ICategoryRepository categoryRepository) : IRequestHandler<CategoryGetAllQuery, ICollection<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        public async Task<ICollection<CategoryResponse>> Handle(CategoryGetAllQuery request, CancellationToken cancellationToken)
        {
            var productList = await _categoryRepository.GetAll<CategoryResponse>(CategoryGetAllQuerySelect);
            return productList;
        }

        private static readonly Expression<Func<Category<byte>, CategoryResponse>> CategoryGetAllQuerySelect = p => new CategoryResponse
        {
            Id = p.Id,
            Name = p.Name,

        };
    }
}
