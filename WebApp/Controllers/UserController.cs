using Entities.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IRepository<User.User> _userRepository;

    public UserController(IRepository<User.User> userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("{id}")]
    public async Task<User.User?> Get(Guid id)
    {
        return await _userRepository.GetAsync(id);
    }
}