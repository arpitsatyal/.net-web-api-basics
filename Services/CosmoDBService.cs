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
        _container = _database.CreateContainerIfNotExistsAsync(containerId, "/id").GetAwaiter().GetResult();
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

    public async Task<webapi.Models.User> AddItemAsync(webapi.Models.AddUsers user)
    {
        var newUser = new webapi.Models.User()
        {
            Id = Guid.NewGuid().ToString(),
            Username = user.Username,
            Email = user.Email,
            Contacts = new List<Contact>()
        };

        ItemResponse<webapi.Models.User> response = await _container.CreateItemAsync(newUser);
        return response.Resource;
    }

   public async Task<webapi.Models.User> EditItemAsync(string userId, webapi.Models.UpdateUsers updatedUser)
    {
    var user = await _container.ReadItemAsync<webapi.Models.User>(userId, new PartitionKey(userId));
    
    user.Resource.Username = updatedUser.Username;
    user.Resource.Email = updatedUser.Email;
    
    ItemResponse<webapi.Models.User> response = await _container.ReplaceItemAsync(user.Resource, userId, new PartitionKey(userId));
    
    return response.Resource;
    }

    public async Task<bool> DeleteItemAsync(string userId)
    {
   ItemResponse<dynamic> response =  await _container.DeleteItemAsync<dynamic>(userId, new PartitionKey(userId));
    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
    {
    return true;
    }
    else
    {
    return false;
    }
    }

    public async Task<webapi.Models.Contact> AddContactToUserAsync(string userId, AddContact contact)
    {
        var newContact = new webapi.Models.Contact()
        {
            Id = Guid.NewGuid().ToString(),
            PhoneNumber = contact.PhoneNumber,
            Address = contact.Address,
            UserId = userId
        };

        ItemResponse<webapi.Models.User> response = await _container.ReadItemAsync<webapi.Models.User>(userId, new PartitionKey(userId));
        webapi.Models.User user = response.Resource;


        user.Contacts.Add(newContact);
        await _container.ReplaceItemAsync(user, user.Id, new PartitionKey(user.Id));
        return newContact;
    }

}

