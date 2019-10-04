using System;
using System.Threading.Tasks;
using DotNetCoreWCF.GrpcClient.Proxies;
using DotNetCoreWCF.GrpcSample.Services;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;

namespace DotNetCoreWCF.GrpcClient
{
	public class Application
	{
		public Application(ILogger<Application> logger, 
			Greeter.GreeterClient greeterClient,
			IEmployeeClientAsync employeeService)
		{
			Logger = logger;
			GreeterClient = greeterClient;
			EmployeeService = employeeService;
		}

		protected ILogger<Application> Logger { get; }
		protected Greeter.GreeterClient GreeterClient { get; }
		protected IEmployeeClientAsync EmployeeService { get; }

		public Task Run()
		{
			Task.WaitAll(RunGreeter(), RunEmployeeAsync(), RunEmployee());

			return Task.CompletedTask;
		}

		private async Task RunGreeter()
		{
			Logger.LogInformation("Running Greeter gRPC Demo");
			Logger.LogInformation("*******************************");

			var reply = await GreeterClient.SayHelloAsync(
							  new HelloRequest { Name = "GreeterClient" });

			Logger.LogInformation("Greeting: " + reply.Message);
			Logger.LogInformation("End Greeter gRPC Demo");
			Logger.LogInformation("*******************************");
		}

		private Task RunEmployee()
		{
			Logger.LogInformation("Running Employee gRPC Demo");
			Logger.LogInformation("*******************************");

			try
			{
				var response = EmployeeService.Get(new Contracts.Model.Employees.EmployeeRequest { ActiveOnly = true });

				Logger.LogInformation($"Employees found: {response?.Employees.Length}");
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, "Oh noes!");
			}
			finally
			{
				Logger.LogInformation("End Employee gRPC Demo");
				Logger.LogInformation("*******************************");
			}

			return Task.CompletedTask;
		}

		private async Task RunEmployeeAsync()
		{
			Logger.LogInformation("Running Employee gRPC Demo (Async)");
			Logger.LogInformation("*******************************");

			try
			{
				var response = await EmployeeService.GetAsync(new Contracts.Model.Employees.EmployeeRequest { ActiveOnly = true });

				Logger.LogInformation($"Employees found (async): {response?.Employees.Length}");
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, "Oh noes (async)!");
			}
			finally
			{
				Logger.LogInformation("End Employee gRPC Demo (Async)");
				Logger.LogInformation("*******************************");
			}
		}
	}
}
