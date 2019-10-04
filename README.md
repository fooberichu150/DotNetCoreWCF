# DotNetCoreWCF

Code in this repository was created for my blog post [Using WCF with DotNetCore](https://www.seeleycoder.com/blog/using-wcf-with-dotnetcore/).

The basic premise is that WCF is not yet a first class citizen in DotNetCore do to some reliance on Windows specific functionality (hence the name Windows Communication Foundation).

This project will show some ways that you can still use WCF in DotNetCore alongside other full framework projects in the same solution.  The blog post goes into detail why this is not a good idea, however.

## Update to include gRPC

Code in this branch was created for my blog post [Migrating WCF to gRPC using .NET Core](https://www.seeleycorder.com/blog/migrating-wcf-to-grpc-netcore/).

gRPC added as both a self-hosted `netcoreapp3.0` application (see `DotNetCoreWCF.GrpcHost`) as well as an IHostedService within our `net472` (full framework - `DotNetCoreWCF.Service`) WCF host.

Several of the projects have been renamed or reorganized in this branch.  `DotNetCoreWCF.Service` previously was named `DotNetCoreWCF.Host`, for example.