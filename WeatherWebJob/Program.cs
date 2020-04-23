using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using WeatherWebJob.Services;

namespace WeatherWebJob
{
	class Program
	{
		static async System.Threading.Tasks.Task Main(string[] args)
		{
			HostBuilder builder = new HostBuilder();

			string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
			builder.UseEnvironment(environmentName);

			builder.ConfigureAppConfiguration((hostingContext, config) =>
			{
				Console.WriteLine($"Current Environment : {environmentName}");

				config.SetBasePath(Directory.GetCurrentDirectory())
					.AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
					.AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
					.AddEnvironmentVariables().Build();
			});

			builder.ConfigureLogging((context, b) =>
			{
				bool isProduction = (context.HostingEnvironment.EnvironmentName.ToUpper() == "PRODUCTION");

				b.SetMinimumLevel(isProduction ? LogLevel.Error : LogLevel.Trace);

				b.AddConsole();
			});

			builder.ConfigureWebJobs(b =>
			{
				b.AddAzureStorageCoreServices();
				b.AddAzureStorage();
				b.AddTimers();
			});

			builder.ConfigureServices((context, services) =>
			{
				services.AddScoped<IMyService, MyTestService>();
			});

			var host = builder.Build();
			using (host)
			{
				await host.RunAsync();
			}
		}
	}
}
