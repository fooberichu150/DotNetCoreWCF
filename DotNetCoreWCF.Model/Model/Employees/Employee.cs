using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DotNetCoreWCF.Contracts.Model.Employees
{
	[DataContract]
	public class Employee
	{
		[DataMember]
		public int? EmployeeId { get; set; }
		[DataMember]
		public string UserName { get; set; }
		[DataMember]
		public string FirstName { get; set; }
		[DataMember]
		public string LastName { get; set; }
		[DataMember]
		public string MiddleInitial { get; set; }

		[DataMember]
		public string EmailAddress { get; set; }
		[DataMember]
		public string PhoneNumber { get; set; }
		[DataMember]
		public string Title { get; set; }
		[DataMember]
		public bool IsActive { get; set; }

		[IgnoreDataMember]
		public string Name => $"{LastName}, {FirstName}";
	}
}
