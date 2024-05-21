
using Microsoft.EntityFrameworkCore;

namespace NG.EF.Common.BaseSeeds
{
    public interface ISqlServerMigrationSeed<T> where T : DbContext
    {
        int SeedOrder { get; }
        Task ExecuteAsync(T darkhorseContext);
    }
}
