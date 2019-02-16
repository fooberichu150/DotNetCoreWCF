using System;
using System.Collections.Generic;
using System.Text;
using DotNetCoreWCF.Contracts.Interfaces;
using DotNetCoreWCF.Contracts.Model.Employees;
using Microsoft.Extensions.Logging;

namespace DotNetCoreWCF.Proxies
{
	public class ManuallyConfiguredEmployeeClient : IEmployeeClient, IDisposable
	{
		public ManuallyConfiguredEmployeeClient(System.ServiceModel.ChannelFactory<IEmployeeService> channelFactory, ILogger logger)
		{
			_logger = logger;

			Channel = channelFactory.CreateChannel();
			// https://blogs.msdn.microsoft.com/wenlong/2007/10/25/best-practice-always-open-wcf-client-proxy-explicitly-when-it-is-shared/
			((System.ServiceModel.IClientChannel)Channel).Open();
		}

		public IEmployeeService Channel { get; }

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

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls
		private readonly ILogger _logger;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// https://stackoverflow.com/a/9063971/1130952
					// https://docs.microsoft.com/en-us/dotnet/framework/wcf/samples/use-close-abort-release-wcf-client-resources
					var client = ((System.ServiceModel.IClientChannel)Channel);
					try
					{
						client.Close();
						_logger.LogInformation($"ManuallyConfiguredEmployeeClient Channel closed");
					}
					catch (System.ServiceModel.CommunicationException e)
					{
						_logger.LogInformation($"ManuallyConfiguredEmployeeClient Channel had communication exception, attempting abort");
						client.Abort();
					}
					catch (TimeoutException e)
					{
						_logger.LogInformation($"ManuallyConfiguredEmployeeClient Channel had timeout exception, attempting abort");
						client.Abort();
					}
					catch (Exception e) { client.Abort(); throw; }

				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~ManuallyConfiguredEmployeeClient() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion
	}
}
