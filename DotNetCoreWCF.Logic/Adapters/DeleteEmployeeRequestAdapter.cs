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

		public Contracts.Model.Employees.DeleteEmployeeRequest ToDomain(Grpc.Services.DeleteEmployeeRequest source)
		{
			return Mapper.Map<Contracts.Model.Employees.DeleteEmployeeRequest>(source);
		}

		public Grpc.Services.DeleteEmployeeRequest ToGrpc(Contracts.Model.Employees.DeleteEmployeeRequest source)
		{
			return Mapper.Map<Grpc.Services.DeleteEmployeeRequest>(source);
		}
	}
}
