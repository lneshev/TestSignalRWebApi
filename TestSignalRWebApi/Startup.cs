using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleInjector;
using TestSignalRWebApi.Hubs;
using TestSignalRWebApi.Infrastructure;

namespace TestSignalRWebApi
{
	public class Startup
	{
		private readonly Container container;

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
			container = new Container();
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddSignalR();
			services.ConfigureSimpleInjector(Configuration, container);
			services.AddCors(options =>
			{
				options.AddPolicy("Default",
					builder =>
					{
						builder
							.WithOrigins("http://localhost:3000")
							.AllowAnyHeader()
							.AllowAnyMethod()
							.AllowCredentials();
					});
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			container.Verify();

			app.UseHttpsRedirection();
			app.UseCors("Default");
			app.UseRouting();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapHub<MyHub>("/signalr");
			});
		}
	}
}