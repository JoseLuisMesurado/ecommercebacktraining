using ecommerce.core.Entities;
using ecommerce.infra.Contexts;
using ecommerce.seeds.HttpRequest;
using Microsoft.EntityFrameworkCore;
using NG.EF.Common.BaseSeeds;

namespace ecommerce.seeds.Sedding
{
    public class ProductSeed(IHttpRequestMigration httpRequestMigration) : ISqlServerMigrationSeed<ECommerceDbContext>
    {
        IHttpRequestMigration _httpRequestMigration = httpRequestMigration;

        public int SeedOrder => 1;

        public async Task ExecuteAsync(ECommerceDbContext currentContext)
        {

            Console.WriteLine($"Run PermissionTypeSeed ");
            try
            {
                var products = await _httpRequestMigration.RequestMockProducts();

                foreach (var product in products.Products)
                {
                    var category = await currentContext.Categories.FirstOrDefaultAsync(x => x.Name == product.Category);

                    if (category == null)
                    {
                        var newCategory = new Category<byte>
                        {
                            Name = product.Category,
                            Description = "None for Now"
                        };
                        await currentContext.Categories.AddAsync(newCategory);
                        await currentContext.SaveChangesAsync();
                        category = newCategory;
                        
                    }

                    var brand = await currentContext.Brands.FirstOrDefaultAsync(x => x.Name == product.Brand);
                    if (brand == null)
                    {
                        var newBrand = new Brand<int>
                        {
                            Name = product.Brand,
                            Description = "None for Now"
                        };
                        await currentContext.Brands.AddAsync(newBrand);
                        await currentContext.SaveChangesAsync();
                        brand = newBrand;
                    }


                    if (!await currentContext.Products.AnyAsync(x => x.Name == product.Title))
                    {
                        var newProduct = new Product<Guid>
                        {
                            Name = product.Title,
                            Description = product.Description,
                            Price = product.Price,
                            Stock = product.Stock,
                            Thumbnail = product.Thumbnail,
                            BrandId = brand.Id,
                            CategoryId = category.Id,
                            ProductImages = new List<ProductImage<Guid>>(product.Images.Select(d => new ProductImage<Guid>
                            {
                                Url = d
                            }))
                        };

                        await currentContext.Products.AddAsync(newProduct);
                    };

                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
