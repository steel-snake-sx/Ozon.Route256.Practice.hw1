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
        serviceCollection.AddGrpcReflection();

        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseRouting();
        applicationBuilder.UseSwagger();
        applicationBuilder.UseSwaggerUI();

        applicationBuilder.UseEndpoints(endpoinRouteBuilder =>
        {
            endpoinRouteBuilder.MapGet("", () => "Hello World!");
            endpoinRouteBuilder.MapGrpcReflectionService();
            endpoinRouteBuilder.MapGrpcService<CustomersService>();
        });
        
    }
}