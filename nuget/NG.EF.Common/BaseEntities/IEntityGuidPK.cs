namespace NG.EF.Common.BaseEntities
{
    public interface IEntityGuidPK : IBaseEntity
    {
        public Guid Id { get; set; }
    }
}
