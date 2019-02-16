using System;
using DotNetCoreWCF.Proxies.Factories;
using Microsoft.Extensions.Logging;

namespace DotNetCoreWCF.Client
{
	public class EmployeeClientDemo : IEmployeeClientDemo
	{
		private readonly ILogger _logger;
		private readonly IEmployeeClientProxyFactory _clientFactory;

		public EmployeeClientDemo(IEmployeeClientProxyFactory clientFactory, ILogger logger)
		{
			_clientFactory = clientFactory;
			_logger = logger;
		}

		public void Run()
		{
			_logger.LogInformation("Running EmployeeClientDemo");
			_logger.LogInformation("*******************************");

			Contracts.Model.Employees.EmployeeResponse response = null;

			try
			{
				using (var client = _clientFactory.GetClient(ClientType.ManualBindings))
				{
					response = client.Get(new Contracts.Model.Employees.EmployeeRequest { ActiveOnly = true });
				}

				_logger.LogInformation($"Employees found: {response?.Employees.Length}");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Oh noes!");
			}

			_logger.LogInformation("End EmployeeClientDemo");
			_logger.LogInformation("*******************************");
		}
	}
}