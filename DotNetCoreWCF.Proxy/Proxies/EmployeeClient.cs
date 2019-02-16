using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using DotNetCoreWCF.Contracts.Interfaces;
using DotNetCoreWCF.Contracts.Model.Configuration;
using DotNetCoreWCF.Contracts.Model.Employees;
using Microsoft.Extensions.Logging;

namespace DotNetCoreWCF.Proxies
{
	public interface IEmployeeClient : IDisposable, IEmployeeService { }

	public class EmployeeClient : ClientBase<IEmployeeService>, IEmployeeClient //, IEmployeeService
	{
		public EmployeeClient()
			: base() { }

		public EmployeeClient(System.ServiceModel.Channels.Binding binding, EndpointAddress remoteAddress)
			: base(binding, remoteAddress)
		{
		}

		public DeleteEmployeeResponse Delete(DeleteEmployeeRequest request)
		{
			return Channel.Delete(request);
		}

		public EmployeeResponse Get(EmployeeRequest request)
		{
			return Channel.Get(request);
		}

		public Employee UpdateEmployee(Employee employee)
		{
			return Channel.UpdateEmployee(employee);
		}
	}
}
