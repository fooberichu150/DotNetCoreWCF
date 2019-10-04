using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Unity;

namespace DotNetCoreWCF.Host.Services
{
	public class GrpcServiceEndpointRegistry
	{
		public GrpcServiceEndpointRegistry(IUnityContainer container)
		{
			Container = container;
		}

		private IUnityContainer Container { get; }

		public ServerServiceDefinition[] GetServiceDefinitions()
		{
			List<ServerServiceDefinition> serviceDefinitions = new List<ServerServiceDefinition>();

			serviceDefinitions.Add(GrpcSample.Services.EmployeeService.BindService(Container.Resolve<EmployeeServiceEndpoint>()));
			serviceDefinitions.Add(GrpcSample.Services.Greeter.BindService(Container.Resolve<GreeterServiceEndpoint>()));

			return serviceDefinitions.ToArray();
		}
	}
}
