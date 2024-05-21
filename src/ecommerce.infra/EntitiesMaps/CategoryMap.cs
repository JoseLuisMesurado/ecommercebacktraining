using ecommerce.core.Entities;
using ecommerce.infra.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ecommerce.infra.EntitiesMaps
{
    internal class CategoryMap : IEntityTypeConfiguration<Category<byte>>
    {
        public void Configure(EntityTypeBuilder<Category<byte>> builder)
        {
            //Table Name
            builder.ToTable(SqlServerTableConstant.Category);

            //PK
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            //Properties
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(100).IsRequired();

            //Audit Items
            builder.Property(x => x.CreatedBy).HasMaxLength(100).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();

            builder.Property(x => x.UpdatedBy).HasMaxLength(100);
            builder.Property(x => x.UpdatedDate);

            builder.Property(x => x.DeletedBy).HasMaxLength(100);
            builder.Property(x => x.DeletedDate);

            //Navegation One to Many
            builder.HasMany<Product<Guid>>(e => e.Products).WithOne(c => c.Category).HasForeignKey(e => e.CategoryId).IsRequired();
        }
    }
}
