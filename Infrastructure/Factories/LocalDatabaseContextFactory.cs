using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Context;

namespace Infrastructure.Factories;

public class LocalDatabaseContextFactory : IDesignTimeDbContextFactory<LocalDatabaseContext>
{
    public LocalDatabaseContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<LocalDatabaseContext>();
        optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Projects\SQL-EFC\Infrastructure\Data\local_database.mdf;Integrated Security=True;Connect Timeout=30");

        return new LocalDatabaseContext(optionsBuilder.Options);
    }
}
