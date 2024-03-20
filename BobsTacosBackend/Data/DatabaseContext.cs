using BobsTacosBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BobsTacosBackend.Data
{
    public class DatabaseContext : IdentityUserContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {

        }

        public DbSet<MenuItem> MenuItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey(ul => new { ul.UserId, ul.LoginProvider, ul.ProviderKey });

            modelBuilder.Entity<IdentityUserToken<string>>()
                .HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });

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
