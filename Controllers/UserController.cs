using Microsoft.AspNetCore.Mvc;
using webapi.Services;
using webapi.Models;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly CosmosDbService _cosmosDbService;

    public UserController(CosmosDbService cosmosDbService)
    {
        _cosmosDbService = cosmosDbService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var query = "SELECT * FROM c";
        var items = await _cosmosDbService.GetItemsAsync(query);

        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> Post(AddUsers addUsers)
    {
        var newUser = await _cosmosDbService.AddItemAsync(addUsers);
        return Ok(newUser);
    }


    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Put([FromRoute] string id, UpdateUsers updateUsers)
    {
        var updatedUser = await _cosmosDbService.EditItemAsync(id, updateUsers);
        return Ok(updatedUser);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        var updatedUser = await _cosmosDbService.DeleteItemAsync(id);
        return Ok(updatedUser);
    }

    [HttpPost]
    [Route("contact/{id}")]
    public async Task<IActionResult> CreateContact([FromRoute] string id, AddContact addContact)
    {
        var newContact = await _cosmosDbService.AddContactToUserAsync(id, addContact);
        return Ok(newContact);
    }
  
}
