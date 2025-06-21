using Microsoft.AspNetCore.Server.Kestrel.Core;
using Ozon.Route256.Practice.CustomerService;

const string ROUTE256_GRPC_PORT = "ROUTE256_GRPC_PORT";
const string ROUTE256_HTTP_PORT = "ROUTE256_HTTP_PORT";

await Host
    .CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(
        builder => builder.UseStartup<Startup>()
            .ConfigureKestrel(
                option =>
                {
                    option.ListenPortByOptions(ROUTE256_GRPC_PORT, HttpProtocols.Http2);
                    option.ListenPortByOptions(ROUTE256_HTTP_PORT, HttpProtocols.Http1);
                }))
    .Build()
    .RunAsync();