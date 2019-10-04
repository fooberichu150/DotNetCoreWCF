using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetCoreWCF.Contracts.Model.Employees;
using DotNetCoreWCF.Service.Core.Store;

namespace DotNetCoreWCF.Service.Core.Handlers
{
	public class DeleteEmployeeRequestHandler : IDeleteEmployeeRequestHandler
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