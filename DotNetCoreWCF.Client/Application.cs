using System;
using DotNetCoreWCF.Client.Configuration;
using Microsoft.Extensions.Logging;

namespace DotNetCoreWCF.Client
{
	public class Application
	{
		private readonly Func<string, IEmployeeClientDemo> _demoAccessor;
		private readonly ILogger _logger;

		public Application(ILogger logger, Func<string, IEmployeeClientDemo> demoAccessor)
		{
			_logger = logger;
			_demoAccessor = demoAccessor;
            //This is test
		}

		public void Run()
		{
			_logger.LogInformation("I'm running yo!");

			RunDemo1();

			RunDemo2();

			RunDemo3();

			RunDemo4();
		}

		private void RunDemo1()
		{
			try
			{
				var demo = _demoAccessor(DemoConstants.EmployeeClientDemo);
				demo.Run();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ach!");
			}
		}

		private void RunDemo2()
		{
			try
			{
				var demo = _demoAccessor(DemoConstants.FactoryEmployeeClientDemo);
				demo.Run();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ach!");
			}
		}

		private void RunDemo3()
		{
			try
			{
				var demo = _demoAccessor(DemoConstants.AutomaticConfigurationEmployeeClientDemo);
				demo.Run();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ach!");
			}
		}

		private void RunDemo4()
		{
			try
			{
				var demo = _demoAccessor(DemoConstants.WcfGeneratedServiceDemo);
				demo.Run();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ach!");
			}
		}
	}
}