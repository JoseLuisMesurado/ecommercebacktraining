namespace NG.EF.Common.AuditEntities
{
    public interface IAuditableEntity : IAuditableCreate
    {
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
