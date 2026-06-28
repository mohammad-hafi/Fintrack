using fintrack.Models;

namespace fintrack.ViewModels;

public class DashboardViewModel
{
    public User? User { get; set; }

    public List<Transaction> Transactions { get; set; } = [];
}
