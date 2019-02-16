using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using DotNetCoreWCF.Contracts.Model.Employees;

namespace DotNetCoreWCF.Contracts.Interfaces
{
	[ServiceContract]
	public interface IEmployeeService
	{
		[OperationContract]
		EmployeeResponse Get(EmployeeRequest request);

		[OperationContract]
		Employee UpdateEmployee(Employee employee);

		[OperationContract]
		DeleteEmployeeResponse Delete(DeleteEmployeeRequest request);
	}
}
