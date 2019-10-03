using System;

namespace DotNetCoreWCF.Logic.Adapters
{
	public interface IEmployeeAdapter
	{
		Contracts.Model.Employees.Employee ToDomain(Grpc.Services.Employee source);
		Grpc.Services.Employee ToGrpc(Contracts.Model.Employees.Employee source);
	}
}
