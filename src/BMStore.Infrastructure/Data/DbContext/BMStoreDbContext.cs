using Microsoft.EntityFrameworkCore;

namespace BMStore.Infrastructure.Data.DbContext;

public class BMStoreDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public BMStoreDbContext(DbContextOptions<BMStoreDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}