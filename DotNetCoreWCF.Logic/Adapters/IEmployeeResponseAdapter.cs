using System;

namespace DotNetCoreWCF.Logic.Adapters
{
	public interface IEmployeeResponseAdapter
	{
		Contracts.Model.Employees.EmployeeResponse ToDomain(GrpcSample.Services.EmployeeResponse source);
		GrpcSample.Services.EmployeeResponse ToGrpc(Contracts.Model.Employees.EmployeeResponse source);
	}
}
