using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DotNetCoreWCF.Contracts.Interfaces;
using DotNetCoreWCF.Contracts.Model.Employees;
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
			IMapper mapper)
		{
			Logger = logger;
			ServiceClient = serviceClient;
			Mapper = mapper;
		}

		protected ILogger<EmployeeClient> Logger { get; }
		protected GrpcModels.EmployeeService.EmployeeServiceClient ServiceClient { get; }
		protected IMapper Mapper { get; }

		public DeleteEmployeeResponse Delete(DeleteEmployeeRequest request)
		{
			var response = ServiceClient.Delete(Mapper.Map<GrpcModels.DeleteEmployeeRequest>(request));

			return Mapper.Map<DeleteEmployeeResponse>(response);
		}

		//public void Dispose()
		//{
		//	throw new NotImplementedException();
		//}

		public EmployeeResponse Get(EmployeeRequest request)
		{
			var response = ServiceClient.Get(Mapper.Map<GrpcModels.EmployeeRequest>(request));

			return Mapper.Map<EmployeeResponse>(response);
		}

		public Employee UpdateEmployee(Employee employee)
		{
			var response = ServiceClient.UpdateEmployee(Mapper.Map<GrpcModels.Employee>(employee));

			return Mapper.Map<Employee>(response);
		}
	}
}