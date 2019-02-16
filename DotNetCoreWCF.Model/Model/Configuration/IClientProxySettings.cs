using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreWCF.Contracts.Model.Configuration
{
	public interface IClientProxySettings
	{
		bool Enabled { get; }

		string Address { get; }

		string DnsIdentity { get; }

		TimeSpan? CloseTimeout { get; }

		TimeSpan? OpenTimeout { get; }

		TimeSpan? ReceiveTimeout { get; }

		TimeSpan? SendTimeout { get; }

		long? MaxBufferPoolSize { get; }

		int? MaxBufferSize { get; }

		int? MaxItemsInObjectGraph { get; }

		long? MaxReceivedMessageSize { get; }

		int? MaxArrayLength { get; }

		int? MaxBytesPerRead { get; }

		int? MaxDepth { get; }

		int? MaxNameTableCharCount { get; }

		int? MaxStringContentLength { get; }

		string SecurityMode { get; }

		System.ServiceModel.TransferMode? TransferMode { get; }
	}

	public interface IBasicHttpClientProxySettings : IClientProxySettings
	{
		bool? AllowCookies { get; }

		bool? BypassProxyOnLocal { get; }

		string TextEncoding { get; }

		bool? UseDefaultWebProxy { get; }

		//System.ServiceModel.BasicHttpSecurityMode? SecurityMode { get; }
	}
}
