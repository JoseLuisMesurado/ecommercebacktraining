using NG.EF.Common.AuditEntities;
using NG.EF.Common.BaseEntities;

namespace ecommerce.core.Entities
{
    public class Product<T> : AuditableEntity, IEntityPK<T>
    {
        //Keys
        public T Id { get; set; }
        public byte CategoryId { get; set; }
        public int BrandId { get; set; }

        //properties
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Thumbnail { get; set; }
        public int Stock { get; set; }

        //Navigation properties
        public Brand<int> Brand { get; set; }
        public Category<byte> Category { get; set; }
        public ICollection<ProductImage<Guid>> ProductImages { get; set; }
    }
}
