using NG.EF.Common.AuditEntities;
using NG.EF.Common.BaseEntities;

namespace ecommerce.core.Entities
{
    public class ProductImage<T> : AuditableEntity, IEntityPK<T>
    {
        //Keys
        public T Id { get; set; }
        public Guid ProductId { get; set; }

        //properties
        public string Url { get; set; }

        //navigation properties
        public Product<Guid> Product { get; set; }

    }
}
