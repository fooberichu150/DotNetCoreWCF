using System;
using System.Collections.Generic;
using System.Text;
using DotNetCoreWCF.Contracts.Interfaces;
using DotNetCoreWCF.GrpcSample.Services;
using DotNetCoreWCF.GrpcClient.Proxies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DotNetCoreWCF.GrpcClient.Configuration
{
	public static class EmployeeServiceConfiguration
	{
		public static IServiceCollection RegisterEmployeeService(this IServiceCollection services, IConfiguration configuration)
		{
			// This switch must be set before creating the GrpcChannel/HttpClient.
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

			return services;
		}
	}
}
