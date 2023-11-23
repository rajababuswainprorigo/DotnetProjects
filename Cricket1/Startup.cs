using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Cricket1
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			// Configure services here if needed
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			// Other configurations...

			app.UseStaticFiles(); // This line allows serving static files.

			// Other configurations...
		}
	}
}
