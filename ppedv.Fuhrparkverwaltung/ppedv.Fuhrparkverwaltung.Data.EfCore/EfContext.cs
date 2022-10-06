using Microsoft.EntityFrameworkCore;
using ppedv.Fuhrparkverwaltung.Model;

namespace ppedv.Fuhrparkverwaltung.Data.EfCore
{
    public class EfContext : DbContext
    {
        public DbSet<Auto> Autos { get => Set<Auto>(); }
        public DbSet<Garage> Garagen { get => Set<Garage>(); }

        private readonly string _conString;

        public EfContext(string conString = "Server=(localdb)\\mssqllocaldb;Database=Fuhrpark_dev;Trusted_Connection=true")
        {
            _conString = conString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(_conString);
        }
    }
}