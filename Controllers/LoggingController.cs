using Microsoft.AspNetCore.Mvc;

namespace fintrack.Controllers;

public class LoggingController : Controller
{
    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }
}