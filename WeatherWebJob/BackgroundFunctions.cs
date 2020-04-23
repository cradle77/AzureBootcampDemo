using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherWebJob.Services;

namespace WeatherWebJob
{
	public class BackgroundFunctions
	{
		private readonly IMyService svc;
		private readonly IConfiguration config;

		public BackgroundFunctions(IMyService svc, IConfiguration config)
		{
			this.svc = svc;
			this.config = config;
		}

		// 0 */1 * * * * = every 1 min
		// */30 * * * * * = every 30 secs
		[Singleton]
		public async Task Poll([TimerTrigger("*/30 * * * * *", RunOnStartup = false)]TimerInfo timerInfo, ILogger logger)
		{
			logger.LogInformation($"{nameof(Poll)} - Starting...");

			svc.DoSomething();

			logger.LogInformation($"{nameof(Poll)} - Ended");
		}

	}
}
