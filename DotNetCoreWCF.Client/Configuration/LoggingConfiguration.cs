using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Unity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCoreWCF.Client.Configuration
{
	public static class LoggingConfiguration
	{
		public static IUnityContainer ConfigureLogging(this IUnityContainer container)
		{
			// instantiate and configure logging. Using serilog here, to log to console and a text-file.
			var loggerFactory = new Microsoft.Extensions.Logging.LoggerFactory();
			var loggerConfig = new LoggerConfiguration()
				.WriteTo.Console()
				//.WriteTo.File("logs\\myapp.txt", rollingInterval: RollingInterval.Day)
				.CreateLogger();
			loggerFactory.AddSerilog(loggerConfig);

			// create logger and put it to work.
			var logProvider = loggerFactory.CreateLogger("DotNetCoreWCF.Client");

			container.RegisterInstance<ILoggerFactory>(loggerFactory);
			container.RegisterInstance<Microsoft.Extensions.Logging.ILogger>(logProvider);

			return container;
		}

		public static IServiceCollection ConfigureLogging(this IServiceCollection services)
		{
			// instantiate and configure logging. Using serilog here, to log to console and a text-file.
			var loggerFactory = new Microsoft.Extensions.Logging.LoggerFactory();
			var loggerConfig = new LoggerConfiguration()
				.WriteTo.Console()
				//.WriteTo.File("logs\\myapp.txt", rollingInterval: RollingInterval.Day)
				.CreateLogger();
			loggerFactory.AddSerilog(loggerConfig);

			// create logger and put it to work.
			var logProvider = loggerFactory.CreateLogger("DotNetCoreWCF.Client");

			services.AddSingleton<ILoggerFactory>(loggerFactory);
			services.AddSingleton<Microsoft.Extensions.Logging.ILogger>(logProvider);

			return services;
		}
	}
}
