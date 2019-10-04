using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Unity;
using Microsoft.Extensions.Logging;
using Unity.Microsoft.Logging;

namespace DotNetCoreWCF.Host.Configuration
{
	public static class LoggingConfigurator
	{
		public static IUnityContainer ConfigureLogging(this IUnityContainer container)
		{
			// instantiate and configure logging. Using serilog here to log to console and a text-file.
			var loggerFactory = new Microsoft.Extensions.Logging.LoggerFactory();
			var loggerConfig = new LoggerConfiguration()
				.WriteTo.Console()
				//.WriteTo.File("logs\\myapp.txt", rollingInterval: RollingInterval.Day)
				.CreateLogger();
			loggerFactory.AddSerilog(loggerConfig);

			// create logger and put it to work.
			var logProvider = loggerFactory.CreateLogger("DotNetCoreWCF.Host");

			//container.RegisterInstance<ILoggerFactory>(loggerFactory);
			//container.RegisterInstance<Microsoft.Extensions.Logging.ILogger>(logProvider);

			container.AddExtension(new LoggingExtension(loggerFactory));

			return container;
		}
	}
}
