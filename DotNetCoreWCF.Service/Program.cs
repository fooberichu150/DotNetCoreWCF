using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCoreWCF.Host.Configuration;
using DotNetCoreWCF.Host.Services;
using DotNetCoreWCF.Service.Core.Configuration;

namespace DotNetCoreWCF.Host
{
	class Program
	{
		static void Main(string[] args)
		{
			var container = new Unity.UnityContainer();
			container
				.ConfigureLogging()
				.ConfigureServices()
				.RegisterHandlers();

			var serviceManager = new EmployeeServiceManager(container);
			serviceManager.StartHost();

			var registry = new GrpcServiceEndpointRegistry(container);

			var credentials = Grpc.Core.ServerCredentials.Insecure;
			//new Grpc.Core.SslServerCredentials()
			var serverPort = new Grpc.Core.ServerPort("localhost", 5001, credentials);
			var serverHost = new Grpc.Core.Server();
			foreach (var serviceDefinition in registry.GetServiceDefinitions())
				serverHost.Services.Add(serviceDefinition);
			serverHost.Ports.Add(serverPort);
			serverHost.Start();

			Console.WriteLine($"Service hosts listening at localhost:5001");
			Console.WriteLine("WCF Service Hosted Sucessfully");
			Console.WriteLine("Press any key to exit");
			Console.ReadKey();

			serviceManager.StopHost();
		}
	}
}
