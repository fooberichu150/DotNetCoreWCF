using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DotNetCoreWCF.Contracts.Model.Employees
{
	[DataContract]
	public class DeleteEmployeeRequest
	{
		public int EmployeeId { get; set; }
	}
}
