using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreWCF.Logic.Adapters
{
	public interface IEmployeeRequestAdapter
	{
		Contracts.Model.Employees.EmployeeRequest ToDomain(Grpc.Services.EmployeeRequest source);
		Grpc.Services.EmployeeRequest ToGrpc(Contracts.Model.Employees.EmployeeRequest source);
	}
}
