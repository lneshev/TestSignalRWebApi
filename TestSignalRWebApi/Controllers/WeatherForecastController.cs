using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using TestSignalRWebApi.Hubs;
using TestSignalRWebApi.Interfaces;

namespace TestSignalRWebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private readonly IMyService myService;
		private readonly IHubContext<MyHub> context;

		public WeatherForecastController(IMyService myService, IHubContext<MyHub> context)
		{
			this.myService = myService;
			this.context = context;
		}

		[HttpGet]
		public async Task<string> Get()
		{
			await myService.MyMethod();
			//await context.Clients.All.SendAsync("test", "Hi! SignalR works!"); // This will work, if you comment ".AddControllerActivation()" and this controller doesn't depend on IMyService

			// Return dummy data
			return $"API call to {nameof(WeatherForecastController)} (GET), was successful!";
		}
	}
}