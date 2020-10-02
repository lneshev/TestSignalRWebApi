using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using TestSignalRWebApi.Hubs;
using TestSignalRWebApi.Interfaces;

namespace TestSignalRWebApi.Services
{
	public class MyService : IMyService
	{
		private readonly IHubContext<MyHub> context;

		public MyService(IHubContext<MyHub> context)
		{
			this.context = context;
		}

		public async Task MyMethod()
		{
			await context.Clients.All.SendAsync("test", "Hi! SignalR works!");
		}
	}
}