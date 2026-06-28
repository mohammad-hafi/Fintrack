using System.Diagnostics;
using System.Transactions;
using fintrack.Data;
using fintrack.DTOs;
using fintrack.Models;
using fintrack.ViewModels;
using fintrack.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fintrack.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    public HomeController(ApplicationDbContext context)
{
    _context = context;
}
    public IActionResult Index()
    {
        return View();
    }
    public async Task<IActionResult> Dashboard()
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            return RedirectToAction("Login", "Logging");
        }

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

        if (user == null)
        {
            return RedirectToAction("Login", "Logging");
        }

        var transactions = await _context.Transactions
            .Include(t => t.Sender)
            .Include(t => t.Receiver)
            .Where(t =>
                t.SenderId == userId ||
                t.ReceiverId == userId)
            .OrderByDescending(t => t.Date)
            .ToListAsync();

        var vm = new DashboardViewModel
        {
            User = user,
            Transactions = transactions
        };

        return View(vm);
    }
    public async Task<IActionResult> Transaction()
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            return RedirectToAction("Login", "Logging");
        }

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null)
        {
            return RedirectToAction("Login", "Logging");
        }

        var transactions = await _context.Transactions
            .Include(t => t.Sender)
            .Include(t => t.Receiver)
            .Where(t =>
                t.SenderId == userId ||
                t.ReceiverId == userId)
            .OrderByDescending(t => t.Date)
            .ToListAsync();

        var vm = new DashboardViewModel
        {
            User = user,
            Transactions = transactions
        };

        return View(vm);
    }
    public IActionResult Transfer()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
