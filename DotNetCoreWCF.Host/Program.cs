using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCoreWCF.Host.Services;

namespace DotNetCoreWCF.Host
{
	class Program
	{
		static void Main(string[] args)
		{
			var serviceManager = new EmployeeServiceManager();
			serviceManager.StartHost();

			Console.WriteLine("Service Hosted Sucessfully");
			Console.WriteLine("Press any key to exit");
			Console.ReadKey();

			serviceManager.StopHost();
		}
	}
}
