using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using DotNetCoreWCF.Logic.Adapters;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCoreWCF.Logic.Configuration
{
	public static class Configurator
	{
		public static IServiceCollection ConfigureAdapters(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(Configurator));
			services.AddSingleton<IEmployeeAdapter, EmployeeAdapter>();
			services.AddSingleton<IEmployeeRequestAdapter, EmployeeRequestAdapter>();

			return services;
		}
	}
}
