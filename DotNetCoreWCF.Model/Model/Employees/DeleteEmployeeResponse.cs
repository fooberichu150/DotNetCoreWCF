using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DotNetCoreWCF.Contracts.Model.Employees
{
	[DataContract]
	public class DeleteEmployeeResponse
	{
		[DataMember]
		public int? DeletedEmployeeId { get; set; }
	}
}
