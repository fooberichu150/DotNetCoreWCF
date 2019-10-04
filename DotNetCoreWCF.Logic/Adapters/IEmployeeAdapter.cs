using System;

namespace DotNetCoreWCF.Logic.Adapters
{
	public interface IEmployeeAdapter
	{
		Contracts.Model.Employees.Employee ToDomain(GrpcSample.Services.Employee source);
		GrpcSample.Services.Employee ToGrpc(Contracts.Model.Employees.Employee source);
	}
}
