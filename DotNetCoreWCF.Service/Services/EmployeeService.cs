using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCoreWCF.Contracts.Interfaces;
using DotNetCoreWCF.Contracts.Model.Employees;
using DotNetCoreWCF.Service.Core.Handlers;
using DotNetCoreWCF.Service.Core.Store;
using Microsoft.Extensions.Logging;
using Unity;

namespace DotNetCoreWCF.Host.Services
{
	public class EmployeeService : IEmployeeService
	{
		public EmployeeService(ILogger logger, 
			IDeleteEmployeeRequestHandler deleteEmployeeRequestHandler, 
			IGetEmployeeRequestHandler getEmployeeRequestHandler, 
			IUpdateEmployeeRequestHandler updateEmployeeRequestHandler)
		{
			Logger = logger;
			UpdateEmployeeRequestHandler = updateEmployeeRequestHandler;
			GetEmployeeRequestHandler = getEmployeeRequestHandler;
			DeleteEmployeeRequestHandler = deleteEmployeeRequestHandler;
		}

		protected IUnityContainer Container { get; }
		protected IEmployeeStore EmployeeStore { get; }
		protected ILogger Logger { get; }
		protected IDeleteEmployeeRequestHandler DeleteEmployeeRequestHandler { get; }
		protected IGetEmployeeRequestHandler GetEmployeeRequestHandler { get; }
		protected IUpdateEmployeeRequestHandler UpdateEmployeeRequestHandler { get; }

		public DeleteEmployeeResponse Delete(DeleteEmployeeRequest request)
		{
			var response = DeleteEmployeeRequestHandler.Delete(request);

			return response;
		}

		public EmployeeResponse Get(EmployeeRequest request)
		{
			var response = GetEmployeeRequestHandler.Get(request);

			return response;
		}

		public Employee UpdateEmployee(Employee employee)
		{
			var response = UpdateEmployeeRequestHandler.Update(employee);

			return response;
		}
	}
}