using ecommerce.core.Entities;
using ecommerce.core.ISqlRepositories;
using MediatR;

namespace ecommerce.application.Product.Commands
{
    public class AddProductCommand : IRequest<Product<Guid>>
    {
        public byte CategoryId { get; set; }
        public int BrandId { get; set; } = 1;

        //properties
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Thumbnail { get; set; }
        public int Stock { get; set; }
        public List<String> Images { get; set; }
    }

    public class AddEmployeeCommandHandler : IRequestHandler<AddProductCommand, Product<Guid>>
    {
        private readonly IProductRepository _productRepository;
        public AddEmployeeCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Product<Guid>> Handle(AddProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var toAdd = new Product<Guid>
                {
                    Name = command.Name,
                    Description = command.Description,
                    Price = command.Price,
                    Thumbnail = command.Thumbnail,
                    Stock = command.Stock,
                    CategoryId = command.CategoryId,
                    BrandId = 1,
                    //ProductImages = command.Images.Select(i => new ProductImage<Guid> { Url = i }).ToList()
                };
                await _productRepository.Add(toAdd);
                return toAdd;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }

    //public sealed class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    //{
    //    public AddProductCommandValidator()
    //    {
    //        RuleFor(command => command.BrandId)
    //            .NotEmpty()
    //            .WithMessage("The brand identifier can't be empty.");

    //        RuleFor(command => command.Name)
    //            .NotEmpty()
    //            .WithMessage("The product name method can't be empty.");

    //        RuleFor(command => command.Stock)
    //            .NotNull()
    //            .WithMessage("The stock address can't be empty.");
            
    //    }
    //}
}
