using System;

namespace DotNetCoreWCF.Logic.Adapters
{
	public interface IDeleteEmployeeResponseAdapter
	{
		Contracts.Model.Employees.DeleteEmployeeResponse ToDomain(GrpcSample.Services.DeleteEmployeeResponse source);
		GrpcSample.Services.DeleteEmployeeResponse ToGrpc(Contracts.Model.Employees.DeleteEmployeeResponse source);
	}
}
