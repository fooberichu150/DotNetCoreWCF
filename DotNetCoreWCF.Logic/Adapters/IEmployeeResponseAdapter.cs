using System;

namespace DotNetCoreWCF.Logic.Adapters
{
	public interface IEmployeeResponseAdapter
	{
		Contracts.Model.Employees.EmployeeResponse ToDomain(Grpc.Services.EmployeeResponse source);
		Grpc.Services.EmployeeResponse ToGrpc(Contracts.Model.Employees.EmployeeResponse source);
	}
}
