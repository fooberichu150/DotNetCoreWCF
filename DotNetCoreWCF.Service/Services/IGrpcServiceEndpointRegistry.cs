using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;

namespace DotNetCoreWCF.Host.Services
{
	public interface IGrpcServiceEndpointRegistry
	{
		ServerServiceDefinition[] GetServiceDefinitions();
	}
}
