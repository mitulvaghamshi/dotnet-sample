using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using asignment1.Models;

namespace asignment1.Data
{
    public class asignment1Context : DbContext
    {
        public asignment1Context(DbContextOptions<asignment1Context> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Car>().HasData(
                new Car
                {
                    Id = 1,
                    Make = "Tesla",
                    Model = "Model S",
                    Color = "White",
                    Year = 2020,
                    PurchaseDate = new DateTime(2020, 8, 18),
                    Kilometers = 100_000
                },
                new Car
                {
                    Id = 2,
                    Make = "Ford",
                    Model = "Mustang",
                    Color = "Yellow",
                    Year = 2016,
                    PurchaseDate = new DateTime(2020, 5, 1),
                    Kilometers = 80_500
                },
                new Car
                {
                    Id = 3,
                    Make = "Toyota",
                    Model = "Corolla",
                    Color = "White",
                    Year = 2021,
                    PurchaseDate = new DateTime(2017, 1, 25),
                    Kilometers = 20_250
                },
                new Car
                {
                    Id = 4,
                    Make = "Honda",
                    Model = "Civic",
                    Color = "Blue",
                    Year = 2021,
                    PurchaseDate = new DateTime(2021, 10, 1),
                    Kilometers = 1_230
                }
            );
        }

        public DbSet<Car> Car { get; set; }
    }
}
