using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetCoreWCF.Contracts.Model.Employees;

namespace DotNetCoreWCF.Service.Core.Handlers
{
	public interface IGetEmployeeRequestHandler
	{
		EmployeeResponse Get(EmployeeRequest request);
	}
}