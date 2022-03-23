using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Models.Authentication;

namespace Portfolio.Database;

public class AuthenticationDbContext : IdentityDbContext<ApplicationUser>
{
    #region Construction

    public AuthenticationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    #endregion

    #region Fields

    public DbSet<UserPreferences> UserPreferences { get; set; }

    #endregion

    #region Methods

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //a hasher to hash the password before seeding the user to the db
        var hasher = new PasswordHasher<ApplicationUser>();

        //Seeding the User to AspNetUsers table
        modelBuilder.Entity<ApplicationUser>().HasData(
            new ApplicationUser
            {
                Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                PasswordHash = hasher.HashPassword(null, "Admin")
            }
        );
    }

    #endregion
}

