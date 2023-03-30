using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

Console.WriteLine("Minimum Web Server - Kestrel");

new WebHostBuilder()
	.UseKestrel()
	.Configure(app => app.Run(context => context.Response.WriteAsync("<h1>Running... Minimum Web Server - Kestrel</h1>")))
	.Build()
	.Run();
