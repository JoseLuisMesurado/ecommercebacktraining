using ecommerce.core.Entities;
using ecommerce.infra.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce.infra.EntitiesMaps
{
    public class ProductImagesMap : IEntityTypeConfiguration<ProductImage<Guid>>
    {
        public void Configure(EntityTypeBuilder<ProductImage<Guid>> builder)
        {
            //Table Name
            builder.ToTable(SqlServerTableConstant.ProductImage);
            //PK
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            //Properties
            builder.Property(x => x.Url).HasMaxLength(500).IsRequired();

            //Audit Items
            builder.Property(x => x.CreatedBy).HasMaxLength(100).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();

            builder.Property(x => x.UpdatedBy).HasMaxLength(100);
            builder.Property(x => x.UpdatedDate);

            builder.Property(x => x.DeletedBy).HasMaxLength(100);
            builder.Property(x => x.DeletedDate);
        }
    }
}
