using Grpc.Core;

namespace Ozon.Route256.Practice.CustomerService.GrpcServices;

public class CustomersService : Customers.CustomersBase
{
   public override Task<GetCustomersForGeneratorResponse> GetCustomersForGenerator(GetCustomersForGeneratorRequest request, ServerCallContext context)
   {
      var response = new GetCustomersForGeneratorResponse();
      
      return Task.FromResult(response);
   }
}