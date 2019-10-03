using System;

namespace DotNetCoreWCF.Logic.Adapters
{
	public interface IDeleteEmployeeRequestAdapter
	{
		Contracts.Model.Employees.DeleteEmployeeRequest ToDomain(Grpc.Services.DeleteEmployeeRequest source);
		Grpc.Services.DeleteEmployeeRequest ToGrpc(Contracts.Model.Employees.DeleteEmployeeRequest source);
	}
}
