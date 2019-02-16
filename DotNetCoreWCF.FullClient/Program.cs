using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCoreWCF.Client.Configuration;
using Unity;

namespace DotNetCoreWCF.FullClient
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			try
			{
				var container = new UnityContainer();
				container.RegisterType<Application>();
				container
					.ConfigureLogging()
					.RegisterEmployeeService();

				var application = container.Resolve<Application>();
				application.Run();
			}
			finally
			{
				Console.WriteLine("\r\nPress any key to exit...");
				Console.ReadKey();
			}
		}
	}
}
