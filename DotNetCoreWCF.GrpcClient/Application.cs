using System;
using System.Threading.Tasks;
using DotNetCoreWCF.Grpc.Services;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;

namespace DotNetCoreWCF.GrpcClient
{
	public class Application
	{
		public Application(ILogger<Application> logger, 
			Greeter.GreeterClient greeterClient, 
			Contracts.Interfaces.IEmployeeService employeeService)
		{
			Logger = logger;
			GreeterClient = greeterClient;
			EmployeeService = employeeService;
		}

		protected ILogger<Application> Logger { get; }
		protected Greeter.GreeterClient GreeterClient { get; }
		protected Contracts.Interfaces.IEmployeeService EmployeeService { get; }

		public Task Run()
		{
			Task.WaitAll(RunGreeter(), RunEmployee());

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

		private async Task RunEmployee()
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
		}
	}
}
