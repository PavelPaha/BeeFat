using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BeeFat.Data;

public static class ContextFactory
{
    private static DbContextOptions<DbContext> CreateNewContextOptions()
    {
        var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();
        var builder = new DbContextOptionsBuilder<DbContext>();
        builder.UseInMemoryDatabase("db", new InMemoryDatabaseRoot())
            .UseInternalServiceProvider(serviceProvider);
        return builder.Options;
    }

    public static DbContext GeContext()
    {
        return new DbContext(CreateNewContextOptions());
    }
}