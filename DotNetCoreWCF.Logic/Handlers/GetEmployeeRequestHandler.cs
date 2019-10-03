using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetCoreWCF.Contracts.Model.Employees;
using DotNetCoreWCF.Data.Store;

namespace DotNetCoreWCF.GrpcHost.Handlers
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

	public class DeleteEmployeeRequestHandler
	{
		public DeleteEmployeeRequestHandler(IEmployeeStore employeeStore)
		{
			EmployeeStore = employeeStore;
		}

		protected IEmployeeStore EmployeeStore { get; }

		public DeleteEmployeeResponse Delete(DeleteEmployeeRequest request)
		{
			var deletedEmployee = EmployeeStore.Delete(request.EmployeeId);

			return new DeleteEmployeeResponse
			{
				DeletedEmployeeId = deletedEmployee?.EmployeeId
			};
		}
	}
}