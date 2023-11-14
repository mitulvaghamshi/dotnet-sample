using Microsoft.EntityFrameworkCore;
using MovieTracker.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MovieTrackerContext>(options => options.UseSqlite(
    builder.Configuration.GetConnectionString("MovieTrackerContextLite")
        ?? throw new InvalidOperationException("Connection string not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();
else app.UseExceptionHandler("/Home/Error");

app.UseStatusCodePages();

app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/Home/PageNotFound";
        await next();
    }
});

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
