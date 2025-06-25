using Google.Protobuf.Collections;
using Ozon.Route256.Practice.Gen;

namespace Ozon.Route256.Practice.OrdersGenerator.Providers.Customers;

public class CustomerProvider: ICustomerProvider
{
    private readonly Gen.Customers.CustomersClient _client;

    public CustomerProvider(Gen.Customers.CustomersClient client)
    {
        _client = client;
    }
    
    public async Task<CustomerDto> GetRandomCustomer()
    {
        var request = new GetCustomersRequest();
        
        var customersResponse = await _client.GetCustomersAsync(request);

        var count = customersResponse.Customers.Count;
        
        var randomCustomer = customersResponse.Customers[Random.Shared.Next(count)];

        return new CustomerDto
        (
            randomCustomer.Id,
            randomCustomer.FirstName,
            randomCustomer.LastName, 
            ToAddresses(randomCustomer.Addressed)
        );
    }

    private static IEnumerable<AddressDto> ToAddresses(RepeatedField<Address> customerAddressed)
    {
        return customerAddressed.Select(address => new AddressDto(
            address.Region, 
            address.City, 
            address.Street, 
            address.Building, 
            address.Apartment, 
            address.Latitude,
            address.Longitude));
    }
}