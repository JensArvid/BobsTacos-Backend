using BobsTacosBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BobsTacosBackend.Data
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>()
            .HasMany(u => u.FavoriteMenuItems)
            .WithMany(m => m.Users)
            .UsingEntity(j => j.ToTable("UserFavoriteMenuItems"));


            // Customize Identity table names
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles")
                .HasKey(ur => new { ur.UserId, ur.RoleId }); // Define primary key

            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

            modelBuilder.Entity<MenuItem>().HasData(
                new MenuItem
                {
                    id = 1,
                    name = "BobsTaco",
                    price = 4.99f,
                    rating = 5,
                    foodType = "Taco",
                    description = "Indulge in our Daily Specials, where every bite is a burst of flavor. Our signature burger is crispy on the outside and succulently juicy on the inside. Served with a delightful array of chutneys, it''s the perfect choice for your craving.",
                    deliveryTime = 30,
                    image = "https://wallpaperaccess.com/full/4496929.jpg"
                }
                // Add more MenuItem data as needed
            );
        }
    }
}
