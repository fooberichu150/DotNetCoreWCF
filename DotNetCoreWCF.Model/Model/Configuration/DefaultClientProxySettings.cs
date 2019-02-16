using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreWCF.Contracts.Model.Configuration
{
	public class DefaultClientProxySettings : IClientProxySettings, IBasicHttpClientProxySettings
	{
		public string Address { get => throw new NotImplementedException(); }

		public string DnsIdentity { get => throw new NotImplementedException(); }

		public TimeSpan? CloseTimeout { get => throw new NotImplementedException(); }

		public TimeSpan? OpenTimeout { get => throw new NotImplementedException(); }

		public TimeSpan? ReceiveTimeout { get => throw new NotImplementedException(); }

		public TimeSpan? SendTimeout { get => throw new NotImplementedException(); }

		public long? MaxBufferPoolSize { get => throw new NotImplementedException(); }

		public int? MaxBufferSize { get => throw new NotImplementedException(); }

		public int? MaxItemsInObjectGraph { get => throw new NotImplementedException(); }

		public long? MaxReceivedMessageSize { get => throw new NotImplementedException(); }

		public int? MaxArrayLength { get => throw new NotImplementedException(); }

		public int? MaxBytesPerRead { get => throw new NotImplementedException(); }

		public int? MaxDepth { get => throw new NotImplementedException(); }

		public int? MaxNameTableCharCount { get => throw new NotImplementedException(); }

		public int? MaxStringContentLength { get => throw new NotImplementedException(); }

		public string SecurityMode { get => throw new NotImplementedException(); }

		//public System.ServiceModel.SecurityMode? SecurityMode { get => throw new NotImplementedException(); }

		public System.ServiceModel.TransferMode? TransferMode { get => throw new NotImplementedException(); }

		public bool? AllowCookies { get => throw new NotImplementedException(); }

		public bool? BypassProxyOnLocal { get => throw new NotImplementedException(); }

		public string TextEncoding { get => throw new NotImplementedException(); }

		public bool? UseDefaultWebProxy { get => throw new NotImplementedException(); }

		public bool Enabled { get => false; }
	}
}
