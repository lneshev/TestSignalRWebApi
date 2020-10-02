using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using TestSignalRWebApi.Hubs;
using TestSignalRWebApi.Interfaces;
using TestSignalRWebApi.Services;

namespace TestSignalRWebApi.Infrastructure
{
	public static class SimpleInjectorConfiguration
	{
		public static void ConfigureSimpleInjector(this IServiceCollection services, IConfiguration configuration, Container container)
		{
			services.AddSimpleInjector(container, options =>
			{
				options.Container.Options.DefaultLifestyle = Lifestyle.Scoped;
				options.Container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

				// Allows hubs to be registered as Scoped while having transient dependencies.
				options.Container.Options.UseLoosenedLifestyleMismatchBehavior = true;

				options
					.AddAspNetCore()
					.AddControllerActivation();

				//options.CrossWire<IHubContext<MyHub>>();  // No idea if we need this?!
			});

			// Find all Hub implementations and register them as scoped.
			var types = container.GetTypesToRegister<Hub>(typeof(MyHub).Assembly);
			foreach (var type in types)
			{
				container.Register(type, type, Lifestyle.Scoped);
			}

			// NOTE: SimpleInjectorHubActivator<T> must be registered as Scoped
			services.AddScoped(typeof(IHubActivator<>), typeof(SimpleInjectorHubActivator<>));


			// Register other services
			container.Register<IMyService, MyService>(Lifestyle.Scoped);

			services
				.BuildServiceProvider(validateScopes: true)
				.UseSimpleInjector(container);

			services.UseSimpleInjectorAspNetRequestScoping(container);
		}
	}
}