using System;
using System.Collections.Generic;
using System.Text;
using EmployeeService;
using Microsoft.Extensions.Logging;

namespace DotNetCoreWCF.Client
{
	public class WcfGeneratedServiceDemo : IEmployeeClientDemo
	{
		private readonly ILogger _logger;

		public WcfGeneratedServiceDemo(ILogger logger)
		{
			_logger = logger;
		}

		public void Run()
		{
			_logger.LogInformation("Running WcfGeneratedServiceDemo");
			_logger.LogInformation("*******************************");

			Contracts.Model.Employees.EmployeeResponse response = null;

			try
			{
				using (var client = new EmployeeServiceClient())
				{
					response = AsyncHelper.RunSync(() => client.GetAsync(new Contracts.Model.Employees.EmployeeRequest { ActiveOnly = true }));
				}

				_logger.LogInformation($"Employees found: {response?.Employees.Length}");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Oh noes!");
			}

			_logger.LogInformation("End WcfGeneratedServiceDemo");
			_logger.LogInformation("*******************************");
		}
	}
}