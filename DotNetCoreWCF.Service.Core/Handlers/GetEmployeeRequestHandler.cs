using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetCoreWCF.Contracts.Model.Employees;
using DotNetCoreWCF.Service.Core.Store;

namespace DotNetCoreWCF.Service.Core.Handlers
{
	public class GetEmployeeRequestHandler : IGetEmployeeRequestHandler
	{
		public GetEmployeeRequestHandler(IEmployeeStore employeeStore)
		{
			EmployeeStore = employeeStore;
		}

		protected IEmployeeStore EmployeeStore { get; }

		public EmployeeResponse Get(EmployeeRequest request)
		{
			return new EmployeeResponse { Employees = EmployeeStore.Get(request).ToArray() };
		}
	}
}