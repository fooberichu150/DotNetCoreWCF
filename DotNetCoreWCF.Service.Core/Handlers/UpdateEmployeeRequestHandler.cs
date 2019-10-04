using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetCoreWCF.Contracts.Model.Employees;
using DotNetCoreWCF.Service.Core.Store;

namespace DotNetCoreWCF.Service.Core.Handlers
{
	public class UpdateEmployeeRequestHandler : IUpdateEmployeeRequestHandler
	{
		public UpdateEmployeeRequestHandler(IEmployeeStore employeeStore)
		{
			EmployeeStore = employeeStore;
		}

		protected IEmployeeStore EmployeeStore { get; }

		public Employee Update(Employee employee)
		{
			throw new NotImplementedException();
		}
	}
}