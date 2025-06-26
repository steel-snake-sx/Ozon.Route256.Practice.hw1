namespace Ozon.Route256.Practice.OrdersGenerator.Providers.Customers;

public record CustomerDto(
    long Id,
    string FirstName,
    string LastName,
    AddressDto Address);