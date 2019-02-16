using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreWCF.Proxies.Factories
{
	public interface IClientProxyFactory<T> where T : new()
	{
		T GetClient();
	}
}
