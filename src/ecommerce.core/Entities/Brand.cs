using NG.EF.Common.AuditEntities;
using NG.EF.Common.BaseEntities;

namespace ecommerce.core.Entities
{
    public class Brand<T> : AuditableEntity, IEntityPK<T>
    {
        //Keys
        public T Id { get; set; }
        //properties
        public string Name { get; set; }
        public string Description { get; set; }
        
        //Navigations
        public ICollection<Product<Guid>> Products { get; set; }
    }
}
