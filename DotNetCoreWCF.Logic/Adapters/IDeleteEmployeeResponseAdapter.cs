using System;

namespace DotNetCoreWCF.Logic.Adapters
{
	public interface IDeleteEmployeeResponseAdapter
	{
		Contracts.Model.Employees.DeleteEmployeeResponse ToDomain(Grpc.Services.DeleteEmployeeResponse source);
		Grpc.Services.DeleteEmployeeResponse ToGrpc(Contracts.Model.Employees.DeleteEmployeeResponse source);
	}
}
