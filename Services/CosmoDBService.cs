using Microsoft.Azure.Cosmos;
using webapi.Models;

namespace webapi.Services;
public class CosmosDbService
{
    private readonly CosmosClient _client;
    private readonly Database _database;
    private readonly Container _container;

    public CosmosDbService(string connectionString, string databaseId, string containerId)
    {
        _client = new CosmosClient(connectionString);
        _database = _client.CreateDatabaseIfNotExistsAsync(databaseId).GetAwaiter().GetResult();
        _container = _database.CreateContainerIfNotExistsAsync(containerId, "/kathmandu").GetAwaiter().GetResult();
    }

  public async Task<IEnumerable<webapi.Models.User>> GetItemsAsync(string query)
{
    var queryDefinition = new QueryDefinition(query);
    var queryResultSetIterator = _container.GetItemQueryIterator<webapi.Models.User>(queryDefinition);

    var results = new List<webapi.Models.User>();

    while (queryResultSetIterator.HasMoreResults)
    {
        var response = await queryResultSetIterator.ReadNextAsync();
        results.AddRange(response.ToList());
    }

    return results;
}
}
