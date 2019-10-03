using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DotNetCoreWCF.Contracts.Interfaces;
using DotNetCoreWCF.Data.Store;
using DotNetCoreWCF.Grpc.Services;
using DotNetCoreWCF.GrpcHost.Handlers;
using DotNetCoreWCF.Logic.Adapters;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Models = DotNetCoreWCF.Contracts.Model.Employees;

namespace DotNetCoreWCF.GrpcHost.Services
{
	public class EmployeeServiceHost : EmployeeService.EmployeeServiceBase//, IEmployeeService
	{
		public EmployeeServiceHost(ILogger<EmployeeServiceHost> logger, 
			IServiceProvider serviceProvider,
			IEmployeeStore employeeStore)
		{
			ServiceProvider = serviceProvider;
			Logger = logger;
			EmployeeStore = employeeStore;
		}

		protected ILogger<EmployeeServiceHost> Logger { get; }
		protected IServiceProvider ServiceProvider { get; }

		protected IGetEmployeeRequestHandler EmployeeRequestHandler { get; }
		protected IEmployeeStore EmployeeStore { get; }
		protected IMapper Mapper { get; }

		public override Task<DeleteEmployeeResponse> Delete(DeleteEmployeeRequest request, ServerCallContext context)
		{
			var mappedRequest = Mapper.Map<Models.DeleteEmployeeRequest>(request);
			var response = (this as IEmployeeService).Delete(mappedRequest);

			return Task.FromResult(Mapper.Map<DeleteEmployeeResponse>(response));
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
			var mappedRequest = Mapper.Map<Models.Employee>(request);
			var response = (this as IEmployeeService).UpdateEmployee(mappedRequest);

			return Task.FromResult(Mapper.Map<Employee>(response));
		}

		//Models.DeleteEmployeeResponse IEmployeeService.Delete(Models.DeleteEmployeeRequest request)
		//{
		//	var deletedEmployee = EmployeeStore.Delete(request.EmployeeId);

		//	return new Models.DeleteEmployeeResponse
		//	{
		//		DeletedEmployeeId = deletedEmployee?.EmployeeId
		//	};

		//	throw new System.NotImplementedException();
		//}

		//Models.EmployeeResponse IEmployeeService.Get(Models.EmployeeRequest request)
		//{
		//	throw new NotImplementedException();
		//}

		//Models.Employee IEmployeeService.UpdateEmployee(Models.Employee employee)
		//{
		//	throw new NotImplementedException();
		//}
	}
}
