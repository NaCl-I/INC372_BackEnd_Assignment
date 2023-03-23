using Microsoft.AspNetCore.Mvc;
using WebAPI.DBContext;
using WebAPI.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    private readonly DatabaseContext _databaseContext;

    public UserController(ILogger<UserController> logger, DatabaseContext databaseContext)
    {
        _logger = logger;
        _databaseContext = databaseContext;
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        try
        {
            var users = _databaseContext.Users.ToList();
            return Ok(new {result = users, message = "success"});
        }
        catch (Exception ex)
        {
            return StatusCode(500, new {result = ex.Message, message = "fail"});
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetUsersById(int id)
    {
        try
        {
            var users = _databaseContext.Users.SingleOrDefault(o => o.Id == id);
            if(users != null){
                return Ok(new {result = users, message = "success"});
            }
            return Ok(new {result = users, message = "empty data"});
        }
        catch (Exception ex)
        {
            return StatusCode(500, new {result = ex.Message, message = "fail"});
        }
    }

    [HttpPost]
    public IActionResult AddUsers(User user)
    {
        try
        {
            _databaseContext.Users.Add(user);
            _databaseContext.SaveChanges();
            return Ok(new {message = "generate success"});
        }
        catch (Exception ex)
        {
            return StatusCode(500, new {result = ex.Message, message = "fail to generate"});
        }        
    }

    [HttpPut]
    public IActionResult UpdateUser(User user)
    {
        try
        {
            var _user = _databaseContext.Users.SingleOrDefault(o => o.Id == user.Id);
            if(_user != null)
            {
                _user.Username = user.Username;
                _user.Password = user.Password;
                _user.Position = user.Position;

                _databaseContext.Users.Update(_user);
                _databaseContext.SaveChanges();
                return Ok(new {message= "update success"});
            }
            else
            {
                return Ok(new {message= "fail to update"});
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new {result = ex.Message, message= "fail to update"});
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        try
        {
            var _user = _databaseContext.Users.SingleOrDefault(o => o.Id == id);
            if(_user != null)
            {
                _databaseContext.Users.Remove(_user);
                _databaseContext.SaveChanges();
                return Ok(new {message= "delete success"});
            }
            else
            {
                return Ok(new {message= "fail to delete"});
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new {result = ex.Message, message= "fail to delete"});
        }
    }
}
