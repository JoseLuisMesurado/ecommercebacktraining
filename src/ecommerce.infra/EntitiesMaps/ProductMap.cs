using ecommerce.core.Entities;
using ecommerce.infra.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce.infra.EntitiesMaps
{
    internal class ProductMap : IEntityTypeConfiguration<Product<Guid>>
    {
        public void Configure(EntityTypeBuilder<Product<Guid>> builder)
        {
            builder.ToTable(SqlServerTableConstant.Product);
            //PK
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            //Properties
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(400).IsRequired();

            //Audit Items
            builder.Property(x => x.CreatedBy).HasMaxLength(100).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();

            builder.Property(x => x.UpdatedBy).HasMaxLength(100);
            builder.Property(x => x.UpdatedDate);

            builder.Property(x => x.DeletedBy).HasMaxLength(100);
            builder.Property(x => x.DeletedDate);

            //Navegation One to Many
            builder.HasMany<ProductImage<Guid>>(e => e.ProductImages).WithOne(c => c.Product).HasForeignKey(e => e.ProductId).IsRequired();
        }
    }
}
