using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BMStore.Infrastructure.Identity.Models;

public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("AspNetUsers");

            // Map ApplicationUser.Email to IdentityUser.Email
            entity.Property(e => e.Email)
                .HasColumnName("Email")
                .HasMaxLength(256);

            entity.Property(e => e.UserName)
                .HasColumnName("UserName")
                .HasMaxLength(256);
        });
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
