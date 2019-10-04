using System;
using AutoMapper;

namespace DotNetCoreWCF.Logic.Adapters
{
	public class DeleteEmployeeRequestAdapter : IDeleteEmployeeRequestAdapter
	{
		public DeleteEmployeeRequestAdapter(IMapper mapper)
		{
			Mapper = mapper;
		}

		protected IMapper Mapper { get; }

		public Contracts.Model.Employees.DeleteEmployeeRequest ToDomain(GrpcSample.Services.DeleteEmployeeRequest source)
		{
			return Mapper.Map<Contracts.Model.Employees.DeleteEmployeeRequest>(source);
		}

		public GrpcSample.Services.DeleteEmployeeRequest ToGrpc(Contracts.Model.Employees.DeleteEmployeeRequest source)
		{
			return Mapper.Map<GrpcSample.Services.DeleteEmployeeRequest>(source);
		}
	}
}
