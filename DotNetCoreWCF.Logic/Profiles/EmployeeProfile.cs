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
			CreateMap<GrpcSample.Services.DeleteEmployeeRequest, Contracts.Model.Employees.DeleteEmployeeRequest>().ReverseMap();
			CreateMap<GrpcSample.Services.Employee, Contracts.Model.Employees.Employee>().ReverseMap();
			CreateMap<GrpcSample.Services.EmployeeRequest, Contracts.Model.Employees.EmployeeRequest>().ReverseMap();
			CreateMap<GrpcSample.Services.DeleteEmployeeResponse, Contracts.Model.Employees.DeleteEmployeeResponse>().ReverseMap();

			CreateMap<Contracts.Model.Employees.EmployeeResponse, GrpcSample.Services.EmployeeResponse>()
				.ForMember(d => d.Employees, opt => opt.UseDestinationValue()).ReverseMap();

			CreateMap<string, string>().ConvertUsing(s => s ?? string.Empty);
		}
	}
}
