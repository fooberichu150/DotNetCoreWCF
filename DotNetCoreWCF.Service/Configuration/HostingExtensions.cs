using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Unity;
using Unity.Microsoft.DependencyInjection;

namespace DotNetCoreWCF.Host.Configuration
{
	public static class HostingExtensions
	{
		private static ServiceProviderFactory _factory;

		public static IHostBuilder UseUnityServiceProvider(this IHostBuilder hostBuilder, IUnityContainer container = null)
		{
			_factory = new ServiceProviderFactory(container);

			return hostBuilder
				.UseServiceProviderFactory<IUnityContainer>(_factory)
				.ConfigureServices((context, services) =>
			{
				services.Replace(ServiceDescriptor.Singleton<IServiceProviderFactory<IUnityContainer>>(_factory));
				services.Replace(ServiceDescriptor.Singleton<IServiceProviderFactory<IServiceCollection>>(_factory));
			});
		}
	}
}
