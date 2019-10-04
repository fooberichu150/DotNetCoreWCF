using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DotNetCoreWCF.Contracts.Interfaces;
using DotNetCoreWCF.Contracts.Model.Employees;
using DotNetCoreWCF.Logic.Adapters;
using Microsoft.Extensions.Logging;
using GrpcModels = DotNetCoreWCF.Grpc.Services;

namespace DotNetCoreWCF.GrpcClient.Proxies
{
	public interface IEmployeeClient: IEmployeeService
	{
		Task<EmployeeResponse> GetAsync(EmployeeRequest request);
		Task<DeleteEmployeeResponse> DeleteAsync(DeleteEmployeeRequest request);
		Task<Employee> UpdateAsync(Employee employee);
	}

	public class EmployeeClient : IEmployeeService
	{
		public EmployeeClient(ILogger<EmployeeClient> logger, 
			GrpcModels.EmployeeService.EmployeeServiceClient serviceClient,
			IDeleteEmployeeRequestAdapter deleteEmployeeRequestAdapter,
			IDeleteEmployeeResponseAdapter deleteEmployeeResponseAdapter,
			IEmployeeRequestAdapter employeeRequestAdapter,
			IEmployeeResponseAdapter employeeResponseAdapter,
			IEmployeeAdapter employeeAdapter)
		{
			Logger = logger;
			ServiceClient = serviceClient;

			DeleteEmployeeRequestAdapter = deleteEmployeeRequestAdapter;
			DeleteEmployeeResponseAdapter = deleteEmployeeResponseAdapter;

			EmployeeAdapter = employeeAdapter;

			EmployeeRequestAdapter = employeeRequestAdapter;
			EmployeeResponseAdapter = employeeResponseAdapter;
		}

		protected ILogger<EmployeeClient> Logger { get; }
		protected GrpcModels.EmployeeService.EmployeeServiceClient ServiceClient { get; }

		public IDeleteEmployeeRequestAdapter DeleteEmployeeRequestAdapter { get; set; }
		public IDeleteEmployeeResponseAdapter DeleteEmployeeResponseAdapter { get; set; }

		public IEmployeeRequestAdapter EmployeeRequestAdapter { get; set; }
		public IEmployeeResponseAdapter EmployeeResponseAdapter { get; set; }

		public IEmployeeAdapter EmployeeAdapter { get; set; }

		public DeleteEmployeeResponse Delete(DeleteEmployeeRequest request)
		{
			var response = ServiceClient.Delete(DeleteEmployeeRequestAdapter.ToGrpc(request));

			return DeleteEmployeeResponseAdapter.ToDomain(response);
		}

		public EmployeeResponse Get(EmployeeRequest request)
		{
			var response = ServiceClient.Get(EmployeeRequestAdapter.ToGrpc(request));

			return EmployeeResponseAdapter.ToDomain(response);
		}

		public Employee UpdateEmployee(Employee employee)
		{
			var response = ServiceClient.UpdateEmployee(EmployeeAdapter.ToGrpc(employee));

			return EmployeeAdapter.ToDomain(response);
		}
	}
}