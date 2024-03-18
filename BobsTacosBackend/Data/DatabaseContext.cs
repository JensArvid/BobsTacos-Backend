using BobsTacosBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BobsTacosBackend.Data
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
           
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MenuItem>().HasData(
               new MenuItem { Id = 1, Name = "BobsTaco", Price = 4.99f, Rating = 5, FoodType = "Taco", Description = "test", deliverytime = 30 });
        }


        public DbSet<MenuItem> MenuItems { get; set; }
        
    }

}
