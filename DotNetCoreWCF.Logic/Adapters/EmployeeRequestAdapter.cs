using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace DotNetCoreWCF.Logic.Adapters
{
	public class EmployeeRequestAdapter : IEmployeeRequestAdapter
	{
		public EmployeeRequestAdapter(IMapper mapper)
		{
			Mapper = mapper;
		}

		protected IMapper Mapper { get; }

		public Contracts.Model.Employees.EmployeeRequest ToDomain(Grpc.Services.EmployeeRequest source)
		{
			return Mapper.Map<Contracts.Model.Employees.EmployeeRequest>(source);
		}

		public Grpc.Services.EmployeeRequest ToGrpc(Contracts.Model.Employees.EmployeeRequest source)
		{
			return Mapper.Map<Grpc.Services.EmployeeRequest>(source);
		}
	}
}
