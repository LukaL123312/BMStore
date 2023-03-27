using BMStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BMStore.Infrastructure.Data.DbContext;

public class BMStoreDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public BMStoreDbContext(DbContextOptions<BMStoreDbContext> options) 
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ProductEntity>()
            .HasMany(x => x.SimilarProducts);

        builder.Entity<UserEntity>()
            .HasMany(x => x.Cart);
        builder.Entity<UserEntity>()
            .HasMany(x => x.FavoritePackages);
        builder.Entity<UserEntity>()
            .HasMany(x => x.OrderedPackages);

        builder.Entity<PackageEntity>()
            .HasMany(x => x.Users);
    }

    public DbSet<UserEntity> UserEntities { get; set; }

}