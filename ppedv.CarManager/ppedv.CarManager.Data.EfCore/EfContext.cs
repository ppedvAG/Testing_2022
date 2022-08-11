using Microsoft.EntityFrameworkCore;
using ppedv.CarManager.Model;

namespace ppedv.CarManager.Data.EfCore
{
    public class EfContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Garage> Garages { get; set; }

        string conString;

        public EfContext(string conString = "Server=(localdb)\\mssqllocaldb;Database=Cars_DEV;Trusted_Connection=true")
        {
            this.conString = conString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(conString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                        .HasOne(x => x.Garage)
                        .WithMany(x => x.Cars)
                        .OnDelete(DeleteBehavior.SetNull);
        }
    }
}