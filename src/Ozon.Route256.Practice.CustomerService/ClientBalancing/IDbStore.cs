namespace Ozon.Route256.Practice.CustomerService.ClientBalancing;

public interface IDbStore
{
    Task UpdateEndpointAsync(IReadOnlyCollection<DbEndpoint> endpoints);
}