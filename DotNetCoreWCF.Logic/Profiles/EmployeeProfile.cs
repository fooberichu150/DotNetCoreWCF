using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace DotNetCoreWCF.Logic.Profiles
{
	public class EmployeeProfile : Profile
	{
		public EmployeeProfile()
		{
			CreateMap<Grpc.Services.DeleteEmployeeRequest, Contracts.Model.Employees.DeleteEmployeeRequest>().ReverseMap();
			CreateMap<Grpc.Services.Employee, Contracts.Model.Employees.Employee>().ReverseMap();
			CreateMap<Grpc.Services.EmployeeRequest, Contracts.Model.Employees.EmployeeRequest>().ReverseMap();
			CreateMap<Grpc.Services.DeleteEmployeeResponse, Contracts.Model.Employees.DeleteEmployeeResponse>().ReverseMap();

			CreateMap<Contracts.Model.Employees.EmployeeResponse, Grpc.Services.EmployeeResponse>()
				.ForMember(d => d.Employees, opt => opt.UseDestinationValue()).ReverseMap();

			CreateMap<string, string>().ConvertUsing(s => s ?? string.Empty);
		}
	}
}
