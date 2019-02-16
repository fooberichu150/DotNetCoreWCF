using System;
using System.Collections.Generic;
using System.Text;
using DotNetCoreWCF.Proxies.Factories;
using Microsoft.Extensions.Logging;

namespace DotNetCoreWCF.Client
{
	public class AutomaticConfigurationEmployeeClientDemo : IEmployeeClientDemo
	{
		private readonly IEmployeeClientProxyFactory _clientFactory;
		private readonly ILogger _logger;

		public AutomaticConfigurationEmployeeClientDemo(IEmployeeClientProxyFactory clientFactory, ILogger logger)
		{
			_clientFactory = clientFactory;
			_logger = logger;
		}

		public void Run()
		{
			_logger.LogInformation("Running AutomaticConfigurationDemo; this demo is expected to fail");
			_logger.LogInformation("*******************************");

			Contracts.Model.Employees.EmployeeResponse response = null;

			try
			{
				using (var client = _clientFactory.GetClient())
				{
					response = client.Get(new Contracts.Model.Employees.EmployeeRequest { ActiveOnly = true });
				}

				_logger.LogInformation($"Employees found: {response?.Employees.Length}");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Oh noes!");
			}

			_logger.LogInformation("End AutomaticConfigurationDemo");
			_logger.LogInformation("*******************************");
		}
	}
}
