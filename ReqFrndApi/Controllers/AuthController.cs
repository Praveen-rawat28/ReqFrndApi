using Microsoft.AspNetCore.Mvc;
using ReqFrndApi.Models;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;

    public AuthController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Route("Registration")]
    public IActionResult Registration(Users user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var obj = _context.Users.FirstOrDefault(x => x.Email == user.Email);
        if (obj == null)
        {
            _context.Users.Add(new Users
            {
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,


            });
            _context.SaveChanges();
            return Ok("User Created");
        }

        else
        {
            return BadRequest("Already exixts");
        }



    }
    [HttpPost]
    [Route("Login")]
    public IActionResult Login(Users login)
    {
        var user = _context.Users.FirstOrDefault(x => x.Email == login.Email && x.Password == login.Password);



        if (user != null)
        {
            return Ok(user);

        }
        else
        {
            return NoContent();
        }
    }
}