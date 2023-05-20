using Microsoft.AspNetCore.Mvc;
using webapi.interfaces;
using webapi.Models;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUser _userRepo;

    public UserController(ILogger<UserController> logger, IUser userRepo)
    {
        _logger = logger;
        _userRepo = userRepo;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_userRepo.GetAll());
    }

    [HttpPost]
    public async Task<IActionResult> Post(AddUsers addUsers)
    {
        var newUser = await _userRepo.Add(addUsers);
        return Ok(newUser);
    }
    [HttpPut]
    [Route("{id:int}")]

    public async Task<IActionResult> Put([FromRoute] int id, UpdateUsers updateUsers)
    {
        var user = await _userRepo.Update(id, updateUsers);
        if (user != null)
        {
            return Ok(user);
        }
        return NotFound();
    }
    [HttpDelete]
    [Route("{id:int}")]

    public async Task<IActionResult> Remove([FromRoute] int id)
    {
        var doesUserExist = await _userRepo.Remove(id);
        if (doesUserExist)
        {
            return Ok(doesUserExist);
        }
        return NotFound();
    }
}
