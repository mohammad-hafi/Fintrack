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
        var userId=HttpContext.Session.GetInt32("UserId");

        if(userId==null){
            return RedirectToAction("Login","Logging");
        }
        var user = await _context.Users.FirstOrDefaultAsync(x=>x.Id == userId);

        var transactions=await _context.Transactions.Include(x=>x.Receiver)
        .Include(x=>x.Sender)
        .Where(x=>
            x.SenderId==userId ||
            x.ReceiverId==userId
        ).OrderByDescending(x=>x.Date).ToListAsync();

        var vm=new DashboardViewModel
        {
            Name=user.Name,
            Balance=user.Balance.ToString(),
            Transactions=transactions
        };
        return View(vm);      
    }
    public IActionResult Transaction()
    {
        return View();
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
