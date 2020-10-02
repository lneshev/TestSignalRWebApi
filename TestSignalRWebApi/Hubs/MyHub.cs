using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace TestSignalRWebApi.Hubs
{
	public class MyHub : Hub
	{
		public override async Task OnConnectedAsync()
		{
			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			await base.OnDisconnectedAsync(exception);
		}
	}
}