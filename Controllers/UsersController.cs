using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private static readonly List<User> Users = [];
    private static int nextUserId = 1;

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        return Ok(Users);
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUserById(int id)
    {
        var user = Users.FirstOrDefault(user => user.Id == id);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public ActionResult<User> CreateUser(User user)
    {
        var createdUser = new User
        {
            Id = nextUserId++,
            FullName = user.FullName,
            Email = user.Email,
            Department = user.Department
        };

        Users.Add(createdUser);

        return CreatedAtAction(
            nameof(GetUserById),
            new { id = createdUser.Id },
            createdUser);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, User updatedUser)
    {
        var user = Users.FirstOrDefault(user => user.Id == id);

        if (user is null)
        {
            return NotFound();
        }

        user.FullName = updatedUser.FullName;
        user.Email = updatedUser.Email;
        user.Department = updatedUser.Department;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = Users.FirstOrDefault(user => user.Id == id);

        if (user is null)
        {
            return NotFound();
        }

        Users.Remove(user);

        return NoContent();
    }

    [HttpGet("test-exception")]
    public IActionResult TestException()
    {
        throw new InvalidOperationException("Test exception for ExceptionHandlingMiddleware.");
    }
}
