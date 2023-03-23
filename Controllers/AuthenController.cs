using Microsoft.AspNetCore.Mvc;
using WebAPI.DBContext;
using WebAPI.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenController : ControllerBase
{
    private readonly ILogger<AuthenController> _logger;

    private readonly DatabaseContext _databaseContext;

    public AuthenController(ILogger<AuthenController> logger, DatabaseContext databaseContext)
    {
        _logger = logger;
        _databaseContext = databaseContext;
    }

    // Log in
    [HttpPost("{username}")]
    public IActionResult LogIn(Logging logging)
    {
        try
        {
            var _logging = _databaseContext.Users.SingleOrDefault(o => o.Username == logging.Username && o.Password == logging.Password);
            if(_logging != null)
            {
                _logging.Status = "login";

                _databaseContext.Users.Update(_logging);
                _databaseContext.SaveChanges();
                return Ok(new {message= "log in success"});
            }
            else
            {
                return Ok(new {message= "fail to log in"});
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new {result = ex.Message, message= "fail to log in"});
        }
    }

    //Log out
    [HttpPut("{username}")]
    public IActionResult LogOut(Logging logging)
    {
        try
        {
            var _logging = _databaseContext.Users.SingleOrDefault(o => o.Username == logging.Username);
            var confirm = true;
            if(_logging != null && confirm == true)
            {
                _logging.Status = "logout";

                _databaseContext.Users.Update(_logging);
                _databaseContext.SaveChanges();
                return Ok(new {message= "log out success"});
            }
            else
            {
                return Ok(new {message= "fail to log out"});
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new {result = ex.Message, message= "fail to log out"});
        }
    }


}