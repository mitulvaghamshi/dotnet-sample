var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseExceptionHandler("/error.html");

if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseRouting();

app.UseFileServer();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
		name: "default", 
		pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();