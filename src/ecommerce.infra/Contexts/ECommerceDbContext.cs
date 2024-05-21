using ecommerce.core.Entities;
using ecommerce.core.IContexts;
using ecommerce.infra.EntitiesMaps;
using Microsoft.EntityFrameworkCore;
using NG.EF.Common.AuditEntities;

namespace ecommerce.infra.Contexts
{
    public class ECommerceDbContext : DbContext, IECommerceDbContext
    {
        public DbSet<Product<Guid>> Products { get; set; }
        public DbSet<Category<byte>> Categories { get; set; }
        public DbSet<Brand<int>> Brands { get; set; }
        public DbSet<ProductImage<Guid>> ProductImages { get; set; }

        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            MapEntitites(modelBuilder);
        }

        private static void MapEntitites(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Product<Guid>>(new ProductMap());
            modelBuilder.ApplyConfiguration<ProductImage<Guid>>(new ProductImagesMap());
            modelBuilder.ApplyConfiguration<Category<byte>>(new CategoryMap());
            modelBuilder.ApplyConfiguration<Brand<int>>(new BrandMap());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            if (ChangeTracker.Entries<IAuditableCreate>().Any())
            {
                foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
                {
                    var currentDate = DateTime.UtcNow;
                    switch (entry.State)
                    {
                        case EntityState.Added:

                            entry.Entity.CreatedBy = "user";
                            entry.Entity.CreatedDate = currentDate;
                            //entry.Entity.UpdatedBy = "user";
                            //entry.Entity.UpdatedDate = currentDate;
                            break;
                        case EntityState.Modified:
                            entry.Entity.UpdatedBy = "user";
                            entry.Entity.UpdatedDate = currentDate;
                            break;
                    }
                }
            }
            else if (ChangeTracker.Entries<IAuditableCreate>().Any())
            {
                foreach (var entry in ChangeTracker.Entries<IAuditableCreate>())
                {
                    var currentDate = DateTime.UtcNow;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.CreatedBy = "user";
                            entry.Entity.CreatedDate = currentDate;
                            break;
                    }
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
