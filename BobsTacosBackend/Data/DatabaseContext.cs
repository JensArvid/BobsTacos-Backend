using BobsTacosBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace BobsTacosBackend.Data
{
    public class DatabaseContext : DbContext
    {

        private string _connectionString;
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnectionString")!;
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<MenuItem>().HasData(
               new MenuItem { Id = 1, Name = "BobsTaco", Price = 4.99f, Rating = 5, FoodType = "Taco", Description = "test", deliverytime = 30 });
        }


        public DbSet<MenuItem> MenuItems { get; set; }
        
    }

}
