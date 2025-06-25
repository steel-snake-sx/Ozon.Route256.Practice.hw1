using Grpc.Core;
using Ozon.Route256.Practice.Gen;

namespace Ozon.Route256.Practice.CustomerService.GrpcServices;

public class CustomersService : Customers.CustomersBase
{
    // Реализация gRPC-метода для предоставления данных клиентов генератору.
    public override Task<GetCustomersForGeneratorResponse> GetCustomersForGenerator(
        GetCustomersForGeneratorRequest request, ServerCallContext context)
    {
        // Пока возвращаем пустой ответ. Будет логика получения данных клиентов, наверное.
        var response = new GetCustomersForGeneratorResponse();
        return Task.FromResult(response);
    }
}