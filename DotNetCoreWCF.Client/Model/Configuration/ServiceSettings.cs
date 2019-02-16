using System;
using System.Collections.Generic;
using System.Text;
using DotNetCoreWCF.Contracts.Model.Configuration;

namespace DotNetCoreWCF.Client.Model.Configuration
{
	public class ServiceSettings : IClientProxySettings
	{
		public bool Enabled { get; set; }
		public string Address { get; set; }
		public string DnsIdentity { get; set; }
		public TimeSpan? CloseTimeout { get; set; }
		public TimeSpan? OpenTimeout { get; set; }
		public TimeSpan? ReceiveTimeout { get; set; }
		public TimeSpan? SendTimeout { get; set; }
		public long? MaxBufferPoolSize { get; set; }
		public int? MaxBufferSize { get; set; }
		public int? MaxItemsInObjectGraph { get; set; }
		public long? MaxReceivedMessageSize { get; set; }
		public int? MaxArrayLength { get; set; }
		public int? MaxBytesPerRead { get; set; }
		public int? MaxDepth { get; set; }
		public int? MaxNameTableCharCount { get; set; }
		public int? MaxStringContentLength { get; set; }
		public string SecurityMode { get; set; }
		public System.ServiceModel.TransferMode? TransferMode { get; set; }
	}

	public class BasicHttpServiceSettings : ServiceSettings, IBasicHttpClientProxySettings
	{
		public bool? AllowCookies { get; set; }
		public bool? BypassProxyOnLocal { get; set; }
		public string TextEncoding { get; set; }
		public bool? UseDefaultWebProxy { get; set; }
	}
}
