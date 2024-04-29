using Microsoft.EntityFrameworkCore;
using QV596H_HFT_2023241.Models;
using System;
using System.Data;
using System.Numerics;

namespace QV596H_HFT_2023241.Repository
{
    public class CarDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public CarDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                object useInMemoryDatabase = builder
                    .UseLazyLoadingProxies()
                    .UseInMemoryDatabase("Car");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brands>()
                .HasMany(b => b.Cars)
                .WithOne(c => c.Brand)
                .HasForeignKey(c => c.BrandId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Car>()
                .HasMany(c => c.RentalEvents)
                .WithOne(r => r.Car)
                .HasForeignKey(r => r.CarId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Brands>().HasData(new
            {
                BrandId = 1,
                BrandName = "Suzuki",
            },
new
{
    BrandId = 2,
    BrandName = "Seat",
},
new
{
    BrandId = 3,
    BrandName = "Ford",
});

            modelBuilder.Entity<Car>().HasData(new Car[]
            {
                new Car { CarId = 1, Model = "SX4", BrandId = 1 },
                new Car { CarId = 2, Model = "Leon", BrandId = 2 },
                new Car { CarId = 3, Model = "Smax", BrandId = 3 },
            });

            modelBuilder.Entity<Rental>().HasData(new Rental[]
            {
                new Rental { RentalId = 1, RentalDate = new DateTime(2023, 1, 1), CarId = 1 },
                new Rental { RentalId = 2, RentalDate = new DateTime(2023, 2, 1), CarId = 2 },
                new Rental { RentalId = 3, RentalDate = new DateTime(2023, 3, 1), CarId = 3 },
            });

        }
    }
}
