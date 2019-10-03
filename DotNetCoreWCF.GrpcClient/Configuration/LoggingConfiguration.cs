using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCoreWCF.GrpcClient.Configuration
{
	public static class LoggingConfiguration
	{
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
			var logProvider = loggerFactory.CreateLogger("DotNetCoreWCF.GrpcClient");

			services.AddSingleton<ILoggerFactory>(loggerFactory);
			services.AddSingleton<Microsoft.Extensions.Logging.ILogger>(logProvider);

			return services;
		}
	}
}
