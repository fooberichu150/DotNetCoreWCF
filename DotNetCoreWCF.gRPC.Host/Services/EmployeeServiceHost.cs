using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DotNetCoreWCF.GrpcSample.Services;
using DotNetCoreWCF.Logic.Adapters;
using DotNetCoreWCF.Service.Core.Handlers;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DotNetCoreWCF.GrpcHost.Services
{
	public class EmployeeServiceHost : EmployeeService.EmployeeServiceBase
	{
		public EmployeeServiceHost(ILogger<EmployeeServiceHost> logger, 
			IServiceProvider serviceProvider)
		{
			ServiceProvider = serviceProvider;
			Logger = logger;
		}

		protected ILogger<EmployeeServiceHost> Logger { get; }
		protected IServiceProvider ServiceProvider { get; }

		protected IGetEmployeeRequestHandler EmployeeRequestHandler { get; }

		public override Task<DeleteEmployeeResponse> Delete(DeleteEmployeeRequest request, ServerCallContext context)
		{
			using (var scope = ServiceProvider.CreateScope())
			{
				var handler = scope.ServiceProvider.GetRequiredService<IDeleteEmployeeRequestHandler>();
				var requestAdapter = scope.ServiceProvider.GetRequiredService<IDeleteEmployeeRequestAdapter>();
				var responseAdapter = scope.ServiceProvider.GetRequiredService<IDeleteEmployeeResponseAdapter>();

				var employees = handler.Delete(requestAdapter.ToDomain(request));

				return Task.FromResult(responseAdapter.ToGrpc(employees));
			}
		}

		public override Task<EmployeeResponse> Get(EmployeeRequest request, ServerCallContext context)
		{
			using (var scope = ServiceProvider.CreateScope())
			{
				var handler = scope.ServiceProvider.GetRequiredService<IGetEmployeeRequestHandler>();
				var employeeRequestAdapter = scope.ServiceProvider.GetRequiredService<IEmployeeRequestAdapter>();
				var employeeResponseAdapter = scope.ServiceProvider.GetRequiredService<IEmployeeResponseAdapter>();

				var employees = handler.Get(employeeRequestAdapter.ToDomain(request));

				return Task.FromResult(employeeResponseAdapter.ToGrpc(employees));
			}
		}

		public override Task<Employee> UpdateEmployee(Employee request, ServerCallContext context)
		{
			using (var scope = ServiceProvider.CreateScope())
			{
				var handler = scope.ServiceProvider.GetRequiredService<IUpdateEmployeeRequestHandler>();
				var requestAdapter = scope.ServiceProvider.GetRequiredService<IEmployeeAdapter>();

				var employees = handler.Update(requestAdapter.ToDomain(request));

				return Task.FromResult(requestAdapter.ToGrpc(employees));
			}
		}
	}
}