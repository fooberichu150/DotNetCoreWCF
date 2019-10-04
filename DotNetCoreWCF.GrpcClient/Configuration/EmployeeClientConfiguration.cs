using System;
using DotNetCoreWCF.Contracts.Interfaces;
using DotNetCoreWCF.GrpcClient.Proxies;
using DotNetCoreWCF.GrpcSample.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCoreWCF.GrpcClient.Configuration
{
	public static class EmployeeClientConfiguration
	{
		public static IServiceCollection RegisterEmployeeService(this IServiceCollection services)
		{
			// This switch must be set before creating the GrpcChannel/HttpClient if we aren't using SSL
			AppContext.SetSwitch(
				"System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

			services.AddGrpcClient<Greeter.GreeterClient>(o =>
			{
				o.Address = new Uri("http://localhost:5001");
			});

			services.AddGrpcClient<EmployeeService.EmployeeServiceClient>(o =>
			{
				o.Address = new Uri("http://localhost:5001");
			});

			services.AddTransient<IEmployeeService, EmployeeClient>();
			services.AddTransient<IEmployeeClientAsync, EmployeeClient>();

			return services;
		}
	}
}
