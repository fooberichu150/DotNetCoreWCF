using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DotNetCoreWCF.Contracts.Model.Employees
{
	[DataContract]
	public class EmployeeResponse
	{
		[DataMember]
		public Employee[] Employees { get; set; }
	}
}
