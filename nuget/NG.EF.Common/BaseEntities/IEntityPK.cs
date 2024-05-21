namespace NG.EF.Common.BaseEntities
{
    public interface IEntityPK<T> : IBaseEntity
    {
        T Id { get; set; }
    }
}
