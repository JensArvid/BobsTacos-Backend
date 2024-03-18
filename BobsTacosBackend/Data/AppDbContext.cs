using BobsTacosBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

/*
namespace BobsTacosBackend.Data
{
    public class AppDbContext : DbContext
    {
        private string _connectionString;
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnectionString")!;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseInMemoryDatabase(databaseName: "Database");
            optionsBuilder.UseNpgsql(_connectionString);
            optionsBuilder.LogTo(message => Debug.WriteLine(message));
        }



        public DbSet<MenuItem> MenuItems { get; set; }

    }
}
*/