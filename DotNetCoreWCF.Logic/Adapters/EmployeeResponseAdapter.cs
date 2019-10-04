using System;
using AutoMapper;

namespace DotNetCoreWCF.Logic.Adapters
{
	public class EmployeeResponseAdapter : IEmployeeResponseAdapter
	{
		public EmployeeResponseAdapter(IMapper mapper)
		{
			Mapper = mapper;
		}

		protected IMapper Mapper { get; }

		public Contracts.Model.Employees.EmployeeResponse ToDomain(GrpcSample.Services.EmployeeResponse source)
		{
			return Mapper.Map<Contracts.Model.Employees.EmployeeResponse>(source);
		}

		public GrpcSample.Services.EmployeeResponse ToGrpc(Contracts.Model.Employees.EmployeeResponse source)
		{
			return Mapper.Map<GrpcSample.Services.EmployeeResponse>(source);
		}
	}
}
