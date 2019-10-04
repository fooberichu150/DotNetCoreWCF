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

		public Contracts.Model.Employees.DeleteEmployeeResponse ToDomain(GrpcSample.Services.DeleteEmployeeResponse source)
		{
			return Mapper.Map<Contracts.Model.Employees.DeleteEmployeeResponse>(source);
		}

		public GrpcSample.Services.DeleteEmployeeResponse ToGrpc(Contracts.Model.Employees.DeleteEmployeeResponse source)
		{
			return Mapper.Map<GrpcSample.Services.DeleteEmployeeResponse>(source);
		}
	}
}
