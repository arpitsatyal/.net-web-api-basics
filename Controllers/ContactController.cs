using Microsoft.AspNetCore.Mvc;
using webapi.Services;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactController : ControllerBase
{
      private readonly CosmosDbService _cosmosDbService; 

    public ContactController(CosmosDbService cosmosDbService)
    {
        _cosmosDbService = cosmosDbService;
    }

       [HttpGet]
        public async Task<IActionResult> GetItemsFromCosmosDb()
        {
            var query = "SELECT * FROM user";
            var items = await _cosmosDbService.GetItemsAsync(query);

            return Ok(items);
        }
}
