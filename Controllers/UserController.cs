using Microsoft.AspNetCore.Mvc;
using webapi.Models;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly AppDbContext dbContext;

    public UserController(ILogger<UserController> logger, AppDbContext dbContext)
    {
        _logger = logger;
        this.dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(
            dbContext.Users.ToList()
        );
    }

    [HttpPost]
    public async Task<IActionResult> Post(AddUsers addUsers)
    {
        var user = new User()
        {
            Id = new Random().Next(),
            Email = addUsers.Email,
            Username = addUsers.Username
        };
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
        return Ok(user);
    }
    [HttpPut]
    [Route("{id:int}")]

    public async Task<IActionResult> Put([FromRoute] int id, UpdateUsers updateUsers)
    {
        var user = await dbContext.Users.FindAsync(id);
        if (user != null)
        {
            user.Email = updateUsers.Email;
            user.Username = updateUsers.Username;
            await dbContext.SaveChangesAsync();
            return Ok(user);
        }
        return NotFound();
    }
    [HttpDelete]
    [Route("{id:int}")]

    public async Task<IActionResult> Remove([FromRoute] int id)
    {
        var user = await dbContext.Users.FindAsync(id);
        if (user != null)
        {
            dbContext.Remove(user);
            await dbContext.SaveChangesAsync();
            return Ok(true);
        }
        return NotFound();
    }
}
