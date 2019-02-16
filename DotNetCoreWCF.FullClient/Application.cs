using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCoreWCF.Client;
using Microsoft.Extensions.Logging;

namespace DotNetCoreWCF.FullClient
{
	public class Application
	{
		private readonly ILogger _logger;
		private readonly EmployeeClientDemo _clientDemo;

		public Application(ILogger logger, EmployeeClientDemo clientDemo)
		{
			_logger = logger;
			_clientDemo = clientDemo;
		}

		public void Run()
		{
			_logger.LogInformation("I'm running yo!");

			RunDemo1();
		}

		private void RunDemo1()
		{
			try
			{
				_clientDemo.Run();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ach!");
			}
		}
	}
}
