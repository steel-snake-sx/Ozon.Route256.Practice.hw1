using Ozon.Route256.Practice.CustomerService.ClientBalancing;
using Ozon.Route256.Practice.CustomerService.GrpcServices;

namespace Ozon.Route256.Practice.CustomerService;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddGrpc();
        // Отображение gRPC, чтобы клиенты могли автоматически определять доступные сервисы.
        serviceCollection.AddGrpcReflection();

        // Настраиваем gRPC-клиент для взаимодействия с SdService, указывая его адрес.
        serviceCollection.AddGrpcClient<Gen.SdService.SdServiceClient>( 
            options =>
            {
                options.Address = new Uri("http://localhost:5500");
            });
        
        // Регистрируем фоновую службу для периодического получения данных от SdService.
        serviceCollection.AddHostedService<SdConsumerHostedService>();

        serviceCollection.AddSingleton<IDbStore, DbStore>();

        // Для анализа API, для упрощения генерации документации.
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder applicationBuilder)
    {
        // Маршрутизация для обработки входящих запросов.
        applicationBuilder.UseRouting();
        applicationBuilder.UseSwagger();
        applicationBuilder.UseSwaggerUI();

        applicationBuilder.UseEndpoints(endpoinRouteBuilder =>
        {
            // Конечные точки приложения, доступные маршруты.
            endpoinRouteBuilder.MapGet("", () => "Hello World!");
            endpoinRouteBuilder.MapGrpcReflectionService();
            endpoinRouteBuilder.MapGrpcService<CustomersService>();
        });
        
    }
}