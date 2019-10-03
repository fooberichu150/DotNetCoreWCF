using System;
using AutoMapper;

namespace DotNetCoreWCF.Logic.Adapters
{
	public class EmployeeAdapter : IEmployeeAdapter
	{
		public EmployeeAdapter(IMapper mapper)
		{
			Mapper = mapper;
		}

		protected IMapper Mapper { get; }

		public Contracts.Model.Employees.Employee ToDomain(Grpc.Services.Employee source)
		{
			return Mapper.Map<Contracts.Model.Employees.Employee>(source);
		}

		public Grpc.Services.Employee ToGrpc(Contracts.Model.Employees.Employee source)
		{
			return Mapper.Map<Grpc.Services.Employee>(source);
		}
	}
}
