using System;

namespace DotNetCoreWCF.Logic.Adapters
{
	public interface IDeleteEmployeeRequestAdapter
	{
		Contracts.Model.Employees.DeleteEmployeeRequest ToDomain(GrpcSample.Services.DeleteEmployeeRequest source);
		GrpcSample.Services.DeleteEmployeeRequest ToGrpc(Contracts.Model.Employees.DeleteEmployeeRequest source);
	}
}
