namespace ecommerce.core.IContexts
{
    public interface IECommerceDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
