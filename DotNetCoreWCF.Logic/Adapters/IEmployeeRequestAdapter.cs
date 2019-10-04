using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreWCF.Logic.Adapters
{
	public interface IEmployeeRequestAdapter
	{
		Contracts.Model.Employees.EmployeeRequest ToDomain(GrpcSample.Services.EmployeeRequest source);
		GrpcSample.Services.EmployeeRequest ToGrpc(Contracts.Model.Employees.EmployeeRequest source);
	}
}
