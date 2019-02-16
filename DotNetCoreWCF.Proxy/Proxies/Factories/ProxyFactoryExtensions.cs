using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCoreWCF.Contracts.Model.Configuration;

namespace DotNetCoreWCF.Proxies.Factories
{
	public static class ProxyFactoryExtensions
	{
		public static System.ServiceModel.ClientBase<TChannel> TrySetMaxItemsInObjectGraph<TChannel>(this System.ServiceModel.ClientBase<TChannel> client, IClientProxySettings settings)
			where TChannel : class
		{
			if (!settings.Enabled)
				return client;

			if (settings.MaxItemsInObjectGraph.HasValue)
				client.Endpoint.TrySetMaxItemsInObjectGraph(settings);

			return client;
		}

		public static void TrySetMaxItemsInObjectGraph(this System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, IClientProxySettings settings)
		{
			if (!settings.Enabled)
				return;

			if (settings.MaxItemsInObjectGraph.HasValue)
			{
				var serializer = serviceEndpoint.Contract.Operations.OfType<System.ServiceModel.Description.DataContractSerializerOperationBehavior>().FirstOrDefault();
				if (serializer != null)
					serializer.MaxItemsInObjectGraph = settings.MaxItemsInObjectGraph.Value;
			}
		}

		public static System.ServiceModel.EndpointAddress GetEndpointAddress(this IClientProxySettings settings)
		{
			if (!settings.Enabled)
				return null;

			var endpointIdentity = new System.ServiceModel.DnsEndpointIdentity(settings.DnsIdentity); // "localhost"
			var endpointAddress = new System.ServiceModel.EndpointAddress(new Uri(settings.Address), endpointIdentity);

			return endpointAddress;
		}

		private static bool TryGetEncoding(this string value, out Encoding encoding)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					encoding = null;
					return false;
				}

				encoding = Encoding.GetEncoding(value);
				return true;
			}
			catch
			{
				encoding = null;
				return false;
			}
		}

		public static System.ServiceModel.Channels.Binding GetBasicHttpBinding(this IBasicHttpClientProxySettings settings) //IBasicHttpClientProxySettings settings)
		{
			if (!settings.Enabled)
				return null;

			var binding = Enum.TryParse(settings.SecurityMode, true, out System.ServiceModel.BasicHttpSecurityMode securityMode)
				? new System.ServiceModel.BasicHttpBinding(securityMode)
				: new System.ServiceModel.BasicHttpBinding();


			if (settings.CloseTimeout.HasValue)
				binding.CloseTimeout = settings.CloseTimeout.Value;

			if (settings.OpenTimeout.HasValue)
				binding.OpenTimeout = settings.OpenTimeout.Value;

			if (settings.ReceiveTimeout.HasValue)
				binding.ReceiveTimeout = settings.ReceiveTimeout.Value;

			if (settings.SendTimeout.HasValue)
				binding.SendTimeout = settings.SendTimeout.Value;

			if (settings.AllowCookies.HasValue)
				binding.AllowCookies = settings.AllowCookies.Value;

			if (settings.BypassProxyOnLocal.HasValue)
				binding.BypassProxyOnLocal = settings.BypassProxyOnLocal.Value;

			//binding.EnvelopeVersion
			//binding.MessageVersion
			//binding.ProxyAddress
			//binding.Scheme

			if (settings.TextEncoding.TryGetEncoding(out Encoding encoding))
				binding.TextEncoding = encoding;

			if (settings.UseDefaultWebProxy.HasValue)
				binding.UseDefaultWebProxy = settings.UseDefaultWebProxy.Value;

			if (settings.MaxBufferPoolSize.HasValue)
				binding.MaxBufferPoolSize = settings.MaxBufferPoolSize.Value;

			if (settings.MaxBufferSize.HasValue)
				binding.MaxBufferSize = settings.MaxBufferSize.Value;

			if (settings.MaxReceivedMessageSize.HasValue)
				binding.MaxReceivedMessageSize = settings.MaxReceivedMessageSize.Value;

			//if (!string.IsNullOrWhiteSpace(settings.SecurityMode))
			//	binding.Security = new System.ServiceModel.BasicHttpSecurity();

			if (settings.TransferMode.HasValue)
				binding.TransferMode = settings.TransferMode.Value;

			// reader quotas settings
			if (settings.MaxArrayLength.HasValue)
				binding.ReaderQuotas.MaxArrayLength = settings.MaxArrayLength.Value;

			if (settings.MaxBytesPerRead.HasValue)
				binding.ReaderQuotas.MaxBytesPerRead = settings.MaxBytesPerRead.Value;

			if (settings.MaxDepth.HasValue)
				binding.ReaderQuotas.MaxDepth = settings.MaxDepth.Value;

			if (settings.MaxNameTableCharCount.HasValue)
				binding.ReaderQuotas.MaxNameTableCharCount = settings.MaxNameTableCharCount.Value;

			if (settings.MaxStringContentLength.HasValue)
				binding.ReaderQuotas.MaxStringContentLength = settings.MaxStringContentLength.Value;

			return binding;
		}

		public static System.ServiceModel.Channels.Binding GetNetTcpBinding(this IClientProxySettings settings) //INetTcpClientProxySettings settings)
		{
			if (!settings.Enabled)
				return null;

			var binding = Enum.TryParse(settings.SecurityMode, true, out System.ServiceModel.SecurityMode securityMode)
				? new System.ServiceModel.NetTcpBinding(securityMode)
				: new System.ServiceModel.NetTcpBinding();

			if (settings.CloseTimeout.HasValue)
				binding.CloseTimeout = settings.CloseTimeout.Value;

			if (settings.OpenTimeout.HasValue)
				binding.OpenTimeout = settings.OpenTimeout.Value;

			if (settings.ReceiveTimeout.HasValue)
				binding.ReceiveTimeout = settings.ReceiveTimeout.Value;

			if (settings.SendTimeout.HasValue)
				binding.SendTimeout = settings.SendTimeout.Value;

			if (settings.MaxBufferPoolSize.HasValue)
				binding.MaxBufferPoolSize = settings.MaxBufferPoolSize.Value;

			if (settings.MaxBufferSize.HasValue)
				binding.MaxBufferSize = settings.MaxBufferSize.Value;

			if (settings.MaxReceivedMessageSize.HasValue)
				binding.MaxReceivedMessageSize = settings.MaxReceivedMessageSize.Value;

			//if (settings.SecurityMode.HasValue)
			//	binding.Security = new System.ServiceModel.NetTcpSecurity { Mode = settings.SecurityMode.Value };

			if (settings.TransferMode.HasValue)
				binding.TransferMode = settings.TransferMode.Value;

			// reader quotas settings
			if (settings.MaxArrayLength.HasValue)
				binding.ReaderQuotas.MaxArrayLength = settings.MaxArrayLength.Value;

			if (settings.MaxBytesPerRead.HasValue)
				binding.ReaderQuotas.MaxBytesPerRead = settings.MaxBytesPerRead.Value;

			if (settings.MaxDepth.HasValue)
				binding.ReaderQuotas.MaxDepth = settings.MaxDepth.Value;

			if (settings.MaxNameTableCharCount.HasValue)
				binding.ReaderQuotas.MaxNameTableCharCount = settings.MaxNameTableCharCount.Value;

			if (settings.MaxStringContentLength.HasValue)
				binding.ReaderQuotas.MaxStringContentLength = settings.MaxStringContentLength.Value;

			return binding;
		}
	}
}