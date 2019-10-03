using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCoreWCF.Contracts.Interfaces;
using DotNetCoreWCF.Contracts.Model.Employees;
using DotNetCoreWCF.Data.Store;
using Microsoft.Extensions.Logging;

namespace DotNetCoreWCF.Host.Services
{
	public class EmployeeService : IEmployeeService
	{
		public EmployeeService(IEmployeeStore employeeStore, ILogger logger)
		{
			EmployeeStore = employeeStore;
			Logger = logger;
		}

		protected IEmployeeStore EmployeeStore { get; }
		protected ILogger Logger { get; }

		public DeleteEmployeeResponse Delete(DeleteEmployeeRequest request)
		{
			var deletedEmployee = EmployeeStore.Delete(request.EmployeeId);

			return new DeleteEmployeeResponse
			{
				DeletedEmployeeId = deletedEmployee?.EmployeeId
			};
		}

		public EmployeeResponse Get(EmployeeRequest request)
		{
			var employees = EmployeeStore.Get(request);

			return new EmployeeResponse
			{
				Employees = employees.ToArray()
			};
		}

		public Employee UpdateEmployee(Employee employee)
		{
			throw new NotImplementedException();
		}
	}
}