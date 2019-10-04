using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DotNetCoreWCF.Host.Configuration;
using Microsoft.Extensions.Logging;
using Unity;
using Unity.Wcf;

namespace DotNetCoreWCF.Host.Services
{
	public class EmployeeServiceManager
	{
		private ServiceHost _employeeServiceHost = null;
		private bool _alreadyLoggedSystemFault;
		private bool _requestStop = false;

		public EmployeeServiceManager()
			:this(new UnityContainer())
		{
		}

		public EmployeeServiceManager(IUnityContainer container)
		{
			Container = container;

			Logger = Container.Resolve<ILogger>();
		}

		public IUnityContainer Container { get; }
		public Microsoft.Extensions.Logging.ILogger Logger { get; }

		public void StartHost()
		{
			StartEmployeeService();
		}

		public void StopHost()
		{
			StopEmployeeService();
		}

		private void StartEmployeeService()
		{
			Logger.LogInformation("Starting Employee WCF ServiceHost");
			_employeeServiceHost = new UnityServiceHost(Container, typeof(EmployeeService));
			_employeeServiceHost.Faulted += _serviceHost_Faulted;
			_employeeServiceHost.Open();
		}

		private void StopEmployeeService()
		{
			Logger.LogInformation("Stopping Employee WCF ServiceHost");
			if (_employeeServiceHost != null)
				_employeeServiceHost.Close();
		}

		/// <summary>
		/// This event is triggered when the ServiceHost throws an exception and stops, NOT when a fault is triggered in a service method
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Args</param>
		private void _serviceHost_Faulted(object sender, EventArgs e)
		{
			if (!_alreadyLoggedSystemFault)
			{
				// Log that there was a Fault, and restart the service.
				Logger.LogInformation("!*!*!*! The real time service faulted with a catostrophic error !*!*!*!");
				Trace.WriteLine("Employee service faulted with a catostrophic error");

				_alreadyLoggedSystemFault = true;
			}

#if !DEBUG
			if (!_requestStop)
				StartEmployeeService();
#endif // DEBUG
		}
	}
}