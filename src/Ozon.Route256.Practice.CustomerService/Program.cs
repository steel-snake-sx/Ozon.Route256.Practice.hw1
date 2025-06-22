using Microsoft.AspNetCore.Server.Kestrel.Core;
using Ozon.Route256.Practice.CustomerService;

const string ROUTE256_GRPC_PORT = "ROUTE256_GRPC_PORT";
const string ROUTE256_HTTP_PORT = "ROUTE256_HTTP_PORT";

// Создаем и запускаем веб-хост с настройками по умолчанию и кастомной конфигурацией.
await Host
    .CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(
        builder => builder.UseStartup<Startup>()
            .ConfigureKestrel(
                option =>
                {
                    // Kestrel для прослушивания gRPC-порта.
                    option.ListenPortByOptions(ROUTE256_GRPC_PORT, HttpProtocols.Http2);
                    // Kestrel для прослушивания HTTP-порта.
                    option.ListenPortByOptions(ROUTE256_HTTP_PORT, HttpProtocols.Http1);
                }))
    .Build()
    .RunAsync();