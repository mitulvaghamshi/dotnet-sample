using DatabaseExplorer.Models.CarRentals;
using Microsoft.EntityFrameworkCore;

namespace DatabaseExplorer.Data;

public class CarContext : DbContext
{
    public CarContext(DbContextOptions<CarContext> options) : base(options) => Database.EnsureCreated();

    public DbSet<Car> Car { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("name=CarRentals");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
}
