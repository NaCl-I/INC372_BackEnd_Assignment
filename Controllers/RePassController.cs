// RePassController

using Microsoft.AspNetCore.Mvc;
using WebAPI.DBContext;
using WebAPI.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class RePassController : ControllerBase
{
    private readonly ILogger<RePassController> _logger;

    private readonly DatabaseContext _databaseContext;

    public RePassController(ILogger<RePassController> logger, DatabaseContext databaseContext)
    {
        _logger = logger;
        _databaseContext = databaseContext;
    }

    [HttpPut]
    public IActionResult RePassword(RePassword repassword)
    {
        try
        {
            var _repassword = _databaseContext.Users.SingleOrDefault(o => o.Username == repassword.Username);
            if(_repassword != null)
            {
                _repassword.Password = repassword.Password;

                _databaseContext.Users.Update(_repassword);
                _databaseContext.SaveChanges();
                return Ok(new {message= "re-password success"});
            }
            else
            {
                return Ok(new {message= "fail to re-password"});
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new {result = ex.Message, message= "fail to re-password"});
        }
    }
}