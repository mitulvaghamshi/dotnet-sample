using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

Console.WriteLine("Minimum Web Server - Kestrel");

var builder = new WebHostBuilder();

builder.UseKestrel();
builder.Configure(app => {
	app.Run(context => {
		return context.Response.WriteAsync("<h1>Running... Minimum Web Server - Kestrel</h1>");
	});
});

builder.Build().Run();
