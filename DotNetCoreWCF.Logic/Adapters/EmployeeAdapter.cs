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

		public Contracts.Model.Employees.Employee ToDomain(GrpcSample.Services.Employee source)
		{
			return Mapper.Map<Contracts.Model.Employees.Employee>(source);
		}

		public GrpcSample.Services.Employee ToGrpc(Contracts.Model.Employees.Employee source)
		{
			return Mapper.Map<GrpcSample.Services.Employee>(source);
		}
	}
}
