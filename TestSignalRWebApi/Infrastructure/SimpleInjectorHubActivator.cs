using Microsoft.AspNetCore.SignalR;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace TestSignalRWebApi.Infrastructure
{
	public sealed class SimpleInjectorHubActivator<T>
		: IHubActivator<T> where T : Hub
	{
		private readonly Container container;
		private Scope scope;

		public SimpleInjectorHubActivator(Container container)
		{
			this.container = container;
		}

		public T Create()
		{
			scope = AsyncScopedLifestyle.BeginScope(container);
			var instance = container.GetInstance<T>();
			return instance;
		}

		public void Release(T hub)
		{
			scope.Dispose();
		}
	}
}