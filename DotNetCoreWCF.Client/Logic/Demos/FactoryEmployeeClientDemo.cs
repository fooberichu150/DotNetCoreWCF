using System;
using DotNetCoreWCF.Proxies.Factories;
using Microsoft.Extensions.Logging;

namespace DotNetCoreWCF.Client
{
	public class FactoryEmployeeClientDemo : IEmployeeClientDemo
	{
		private readonly IEmployeeClientProxyFactory _clientFactory;
		private readonly ILogger _logger;

		public FactoryEmployeeClientDemo(IEmployeeClientProxyFactory clientFactory, ILogger logger)
		{
			_clientFactory = clientFactory;
			_logger = logger;
		}

		public void Run()
		{
			_logger.LogInformation("Running FactoryEmployeeClientDemo");
			_logger.LogInformation("*******************************");

			Contracts.Model.Employees.EmployeeResponse response = null;

			try
			{
				using (var client = _clientFactory.GetClient(ClientType.ChannelFactory))
				{
					response = client.Get(new Contracts.Model.Employees.EmployeeRequest { ActiveOnly = true });
				}

				_logger.LogInformation($"Employees found: {response?.Employees.Length}");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Oh noes!");
			}

			_logger.LogInformation("End FactoryEmployeeClientDemo");
			_logger.LogInformation("*******************************");
		}
	}
}