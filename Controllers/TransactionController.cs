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
    public async Task<IActionResult> Send(int receiverId, decimal amount)
    {
        var senderId=HttpContext.Session.GetInt32("UserId");

        if (senderId == null)
        return RedirectToAction("Login", "Logging");

        var sender= await _context.Users.FindAsync(senderId);
        var receiver= await _context.Users.FindAsync(receiverId);

            if (sender == null || receiver == null)
        return BadRequest("Invalid users");

        if (sender.Balance < amount)
        return BadRequest("Not enough balance");

        sender.Balance -= amount;
        receiver.Balance += amount;

        var tx = new Transaction
        {
        SenderId = sender.Id,
        ReceiverId = receiver.Id,
        Amount = amount,
        Date = DateTime.Now
        };

        _context.Transactions.Add(tx);
        await _context.SaveChangesAsync();

        return RedirectToAction("Dashboard","Home");
    }
}
