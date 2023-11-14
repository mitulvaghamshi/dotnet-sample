var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseExceptionHandler("/error.html");

if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseRouting();

app.UseFileServer();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

// app.UseEndpoints(endpoints =>
// {
// 	_ = endpoints.MapControllerRoute(
// 		name: "default",
// 		pattern: "{controller=Home}/{action=Index}/{id?}");
// });

app.Run();
