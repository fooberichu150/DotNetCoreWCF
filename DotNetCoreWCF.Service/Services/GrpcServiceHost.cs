using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotNetCoreWCF.Host.Services
{
	public class GrpcServiceHost : IHostedService
	{

		public GrpcServiceHost(IGrpcServiceEndpointRegistry endpointRegistry, 
			ILogger<GrpcServiceHost> logger)
		{
			EndpointRegistry = endpointRegistry;
			Logger = logger;
		}

		protected IGrpcServiceEndpointRegistry EndpointRegistry { get; }
		protected ILogger<GrpcServiceHost> Logger { get; }
		private Grpc.Core.Server _serverHost { get; set; }

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			await StartListener();
		}

		public async Task StopAsync(CancellationToken cancellationToken)
		{
			await StopListener();
		}

		private Task StartListener()
		{
			// TODO: set up SSL credentials (this is default in ASP.NET Core implementation)
			// see https://andrewlock.net/exploring-the-new-project-file-program-and-the-generic-host-in-asp-net-core-3/ for more info
			// - drill into https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.0 from there 
			// - then drill into CreateDefaultBuilder and ConfigureWebHostDefaults to see how it loads SSL by default)
			// - see also: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-3.0#kestrel-options
			var credentials = Grpc.Core.ServerCredentials.Insecure; //new Grpc.Core.SslServerCredentials()
			var serverPort = new Grpc.Core.ServerPort("localhost", 5001, credentials);
			_serverHost = new Grpc.Core.Server();

			foreach (var serviceDefinition in EndpointRegistry.GetServiceDefinitions())
				_serverHost.Services.Add(serviceDefinition);
			_serverHost.Ports.Add(serverPort);
			_serverHost.Start();

			Logger.LogInformation($"Service hosts listening at localhost:5001");

			return Task.CompletedTask;
		}

		private Task StopListener()
		{
			return _serverHost.ShutdownAsync();
		}
	}
}
