using fintrack.Models;
using Microsoft.EntityFrameworkCore;
namespace fintrack.ViewModels;

public class DashboardViewModel
{
    public string Name{get;set;}

    public string Balance{get;set;}

    public List<Transaction> Transactions{get;set;}
}
