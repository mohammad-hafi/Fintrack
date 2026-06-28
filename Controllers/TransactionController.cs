using fintrack.Data;
using fintrack.DTOs;
using fintrack.Models;
using fintrack.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace fintrack.Controllers;

public class TransactionController: Controller
{
    private readonly ApplicationDbContext _context;
    public TransactionController(ApplicationDbContext context)
    {
        _context=context;
    }

    [HttpPost]
    public async Task<IActionResult> Send(string receiverEmail, decimal amount)
    {
           var senderId = HttpContext.Session.GetInt32("UserId");

    if (senderId == null)
        return RedirectToAction("Login", "logging");

    var sender = await _context.Users.FindAsync(senderId);

    if (sender == null)
    {
        return RedirectToAction("Login", "Logging");
    }

    var receiver = await _context.Users
        .FirstOrDefaultAsync(x => x.Email == receiverEmail);

    if (receiver == null)
    {
        TempData["Error"] = "Receiver not found";
        return RedirectToAction("Dashboard", "Home");
    }

    if (sender.Email == receiver.Email)
    {
        TempData["Error"] = "You cannot send money to yourself";
        return RedirectToAction("Dashboard", "Home");
    }

    if (amount <= 0)
    {
        TempData["Error"] = "Invalid amount";
        return RedirectToAction("Dashboard", "Home");
    }

    if (sender.Balance < amount)
    {
        TempData["Error"] = "Not enough balance";
        return RedirectToAction("Dashboard", "Home");
    }

    sender.Balance -= amount;
    receiver.Balance += amount;

    var tx = new Transaction
    {
        SenderId = sender.Id,
        ReceiverId = receiver.Id,
        Amount = amount,
        Date = DateTime.UtcNow
    };

    _context.Transactions.Add(tx);
    await _context.SaveChangesAsync();

    TempData["Success"] = "Transfer completed successfully";

    return RedirectToAction("Dashboard", "Home");
    }
}
