using Grpc.Net.ClientFactory;
using Ozon.Route256.Practice.OrdersGenerator.Configuration;
using Ozon.Route256.Practice.OrdersGenerator.Generator;
using Ozon.Route256.Practice.OrdersGenerator.Infrastructure;
using Ozon.Route256.Practice.OrdersGenerator.Models;
using Ozon.Route256.Practice.OrdersGenerator.Providers.Customers;
using Ozon.Route256.Practice.OrdersGenerator.Providers.Goods;

namespace Ozon.Route256.Practice.OrdersGenerator;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddInfrastructure();
        services.AddScoped<ICustomerProvider, CustomerProvider>();
        services.AddScoped<IGoodsProvider, GoodsProvider>();
        services.AddScoped<IOrderGenerator, OrderGenerator>();
        services.AddScoped<LoggingInterceptor>();
        services.AddGrpcClient<Gen.Customers.CustomersClient>(
            options =>
            {
                var url = _configuration.GetValue<string>("ROUTE256_CUSTOMER_ADDRESS");

                if (string.IsNullOrEmpty(url))
                {
                    throw new ArgumentException("Требуется указать переменную окружения ROUTE256_SD_ADDRESS или она пустая");
                }
                
                options.Address = new Uri(url);

                // все клиенты, которые используют данный канал, будут использовать этот интерсептер(встраивается в запрос(что-то делает с запросом))
                
                options.InterceptorRegistrations.Add(
                    new InterceptorRegistration(
                        InterceptorScope.Channel,
                        provider => provider.GetRequiredService<LoggingInterceptor>()));
            });

        services.Configure<KafkaSettings>(o =>
        {
            o.Servers = _configuration.GetValue<string>("ROUTE256_KAFKA_BROKERS");
        });
        services.Configure<OrderGeneratorSettings>(o =>
        {
            o.OrderSource = _configuration.GetValue<OrderSource>("ROUTE256_ORDER_SOURCE");
            o.OrderRequestTopic = _configuration.GetValue<string>("ROUTE256_ORDER_REQUEST_TOPIC");
        });
    }

    public void Configure(IApplicationBuilder app)
    {
        
    }
}