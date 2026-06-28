using fintrack.Data;
using fintrack.Models;
using fintrack.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace fintrack.Controllers;

public class WalletController : Controller
{
    private readonly ApplicationDbContext _context;

    public WalletController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
public IActionResult Deposit()
{
    return View();
}

[HttpPost]
public async Task<IActionResult> Deposit(DepositViewModel model)
{
    if (!ModelState.IsValid)
        return View(model);

    var userId = HttpContext.Session.GetInt32("UserId");

    if (userId == null)
        return RedirectToAction("Login", "Logging");

    var user = await _context.Users.FindAsync(userId);

    user.Balance += model.Amount;

    var transaction = new Transaction
    {
        SenderId = user.Id,
        ReceiverId = user.Id,
        Amount = model.Amount,
        Type = "Deposit"
    };

    _context.Transactions.Add(transaction);

    await _context.SaveChangesAsync();

return RedirectToAction("Dashboard", "Home");}
[HttpGet]
public IActionResult Withdraw()
{
    return View();
}
[HttpPost]
public async Task<IActionResult> Withdraw(WithdrawViewModel model)
{
    if (!ModelState.IsValid)
        return View(model);

    var userId = HttpContext.Session.GetInt32("UserId");

    if (userId == null)
        return RedirectToAction("Login", "Logging");

    var user = await _context.Users.FindAsync(userId);

    if (user.Balance < model.Amount)
    {
        ModelState.AddModelError("", "Insufficient funds");
        return View(model);
    }

    user.Balance -= model.Amount;

    var transaction = new Transaction
    {
        SenderId = user.Id,
        ReceiverId = user.Id,
        Amount = model.Amount,
        Type = "Withdraw"
    };

    _context.Transactions.Add(transaction);

    await _context.SaveChangesAsync();

   return RedirectToAction("Dashboard", "Home");
}
}