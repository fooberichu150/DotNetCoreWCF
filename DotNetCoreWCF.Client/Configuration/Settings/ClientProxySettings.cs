using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using DotNetCoreWCF.Client.Model.Configuration;
using DotNetCoreWCF.Contracts.Model.Configuration;

namespace DotNetCoreWCF.Client.Configuration.Settings
{
	internal static class ServiceSettingConstants
	{
		public const string EmployeeProxySettings = "EmployeeProxySettings";
		public const string EmployeeService = "EmployeeService";
	}

	public class ClientProxySettings : IClientProxySettings
	{
		private ServiceSettings _coreSettings;
		private ServiceSettings _defaultSettings;

		public ClientProxySettings(string configurationKey, Microsoft.Extensions.Options.IOptionsSnapshot<ServiceSettings> settingsSnapshot)
		{
			CoreSettings = settingsSnapshot.Get(configurationKey);
			// gets the default value
			DefaultSettings = settingsSnapshot.Value;
		}

		public bool Enabled { get => true; }

		public string Address { get => CoreSettings.Address /*Settings.Address*/; }

		public string DnsIdentity { get => Logic.StringTool.SelectStringValue(CoreSettings.DnsIdentity, DefaultSettings.DnsIdentity); }

		public TimeSpan? CloseTimeout { get => CoreSettings.CloseTimeout ?? DefaultSettings.CloseTimeout; }

		public TimeSpan? OpenTimeout { get => CoreSettings.OpenTimeout ?? DefaultSettings.OpenTimeout; }

		public TimeSpan? ReceiveTimeout { get => CoreSettings.ReceiveTimeout ?? DefaultSettings.ReceiveTimeout; }

		public TimeSpan? SendTimeout { get => CoreSettings.SendTimeout ?? DefaultSettings.SendTimeout; }

		public long? MaxBufferPoolSize { get => CoreSettings.MaxBufferPoolSize ?? DefaultSettings.MaxBufferPoolSize; }

		public int? MaxBufferSize { get => CoreSettings.MaxBufferSize ?? DefaultSettings.MaxBufferSize; }

		public int? MaxItemsInObjectGraph { get => CoreSettings.MaxItemsInObjectGraph ?? DefaultSettings.MaxItemsInObjectGraph; }

		public long? MaxReceivedMessageSize { get => CoreSettings.MaxReceivedMessageSize ?? DefaultSettings.MaxReceivedMessageSize; }

		public int? MaxArrayLength { get => CoreSettings.MaxArrayLength ?? DefaultSettings.MaxArrayLength; }

		public int? MaxBytesPerRead { get => CoreSettings.MaxBytesPerRead ?? DefaultSettings.MaxBytesPerRead; }

		public int? MaxDepth { get => CoreSettings.MaxDepth ?? DefaultSettings.MaxDepth; }

		public int? MaxNameTableCharCount { get => CoreSettings.MaxNameTableCharCount ?? DefaultSettings.MaxNameTableCharCount; }

		public int? MaxStringContentLength { get => CoreSettings.MaxStringContentLength ?? DefaultSettings.MaxStringContentLength; }

		public string SecurityMode { get => Logic.StringTool.SelectStringValue(CoreSettings.SecurityMode, DefaultSettings.SecurityMode); }

		public TransferMode? TransferMode { get => CoreSettings.TransferMode ?? DefaultSettings.TransferMode; }

		protected ServiceSettings DefaultSettings { get => _defaultSettings; set => _defaultSettings = value; }
		protected ServiceSettings CoreSettings { get => _coreSettings; set => _coreSettings = value; }
	}

	public class BasicHttpProxySettings : ClientProxySettings, IBasicHttpClientProxySettings
	{
		public BasicHttpProxySettings(string configurationKey, Microsoft.Extensions.Options.IOptionsSnapshot<BasicHttpServiceSettings> settingsSnapshot)
			: base(configurationKey, settingsSnapshot)
		{
		}

		protected BasicHttpServiceSettings DefaultHttpSettings { get => DefaultSettings as BasicHttpServiceSettings; }
		protected BasicHttpServiceSettings CoreHttpSettings { get => CoreSettings as BasicHttpServiceSettings; }

		public bool? AllowCookies { get => CoreHttpSettings.AllowCookies ?? DefaultHttpSettings.AllowCookies; }
		public bool? BypassProxyOnLocal { get => throw new NotImplementedException(); }
		public string TextEncoding { get => throw new NotImplementedException(); }
		public bool? UseDefaultWebProxy { get => throw new NotImplementedException(); }
	}
}
