using Microsoft.EntityFrameworkCore;
using MovieTracker.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MovieTrackerContext>(options =>
    // SQL Server provider
    // options.UseSqlServer(builder.Configuration.GetConnectionString("MovieTrackerContext") 

    // SQLite provider
    options.UseSqlite(builder.Configuration.GetConnectionString("MovieTrackerContextLite")
        ?? throw new InvalidOperationException("Connection string 'MovieTrackerContext(Lite)' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) app.UseExceptionHandler("/Home/Error");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
