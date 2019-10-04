using System.Threading.Tasks;
using DotNetCoreWCF.GrpcSample.Services;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace DotNetCoreWCF.Host.Services
{
	public class GreeterServiceEndpoint : Greeter.GreeterBase
	{
		private readonly ILogger _logger;
		public GreeterServiceEndpoint(ILogger logger)
		{
			_logger = logger;
		}

		public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
		{
			return Task.FromResult(new HelloReply
			{
				Message = "Hello " + request.Name
			});
		}
	}
}
