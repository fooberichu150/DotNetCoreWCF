using System;
using System.Collections.Generic;
using System.Text;
using DotNetCoreWCF.Contracts.Interfaces;
using DotNetCoreWCF.Grpc.Services;
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
			services.AddGrpcClient<Greeter.GreeterClient>(o =>
			{
				o.Address = new Uri("https://localhost:5001");
			});

			services.AddGrpcClient<EmployeeService.EmployeeServiceClient>(o =>
			{
				o.Address = new Uri("https://localhost:5001");
			});

			services.AddTransient<IEmployeeService, EmployeeClient>();

			return services;
		}
	}
}
