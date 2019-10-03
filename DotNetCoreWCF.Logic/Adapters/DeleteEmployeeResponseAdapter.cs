using System;
using AutoMapper;

namespace DotNetCoreWCF.Logic.Adapters
{
	public class DeleteEmployeeResponseAdapter : IDeleteEmployeeResponseAdapter
	{
		public DeleteEmployeeResponseAdapter(IMapper mapper)
		{
			Mapper = mapper;
		}

		protected IMapper Mapper { get; }

		public Contracts.Model.Employees.DeleteEmployeeResponse ToDomain(Grpc.Services.DeleteEmployeeResponse source)
		{
			return Mapper.Map<Contracts.Model.Employees.DeleteEmployeeResponse>(source);
		}

		public Grpc.Services.DeleteEmployeeResponse ToGrpc(Contracts.Model.Employees.DeleteEmployeeResponse source)
		{
			return Mapper.Map<Grpc.Services.DeleteEmployeeResponse>(source);
		}
	}
}
