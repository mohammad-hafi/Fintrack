using fintrack.Data;
using fintrack.DTOs;
using fintrack.Models;
using fintrack.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace fintrack.Controllers;
[Controller]
public class SessionController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly PasswordHash _hash;

    public SessionController(ApplicationDbContext context)
    {
        _context=context;
        _hash=new PasswordHash();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm]UserRegister dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var reg = await _context.Users.AnyAsync(x=>x.Email == dto.Email);
        if(reg)
            return BadRequest("Email already exist");

        var user=new User{
            Name = dto.Name,
            Email = dto.Email,
            Password = _hash.Hash(dto.Password),
            Balance = 0
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        HttpContext.Session.SetInt32("UserId", user.Id);

        return RedirectToAction("Dashboard", "Home");
    }



    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] UserLogin dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);

    if (user == null)
        return BadRequest("Invalid credentials");

    var valid = _hash.Verify(dto.Password, user.Password);

    if (!valid)
        return BadRequest("Invalid credentials");

    HttpContext.Session.SetInt32("UserId", user.Id);
    return RedirectToAction("Dashboard", "Home");
}
}
