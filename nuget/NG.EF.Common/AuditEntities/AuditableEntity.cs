namespace NG.EF.Common.AuditEntities
{
    public class AuditableEntity : AuditableCreate , IAuditableEntity
    {
        public  DateTime? UpdatedDate { get; set; } 
        public  string? UpdatedBy { get; set; }
    }
}
