using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DotNetCoreWCF.Contracts.Model.Employees
{
	[DataContract]
	public class EmployeeRequest
	{
		[DataMember]
		public int? EmployeeId { get; set; }
		[DataMember]
		public string FirstName { get; set; }
		[DataMember]
		public string LastName { get; set; }
		[DataMember]
		public bool ActiveOnly { get; set; }
	}
}
