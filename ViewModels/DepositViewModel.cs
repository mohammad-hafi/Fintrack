using System.ComponentModel.DataAnnotations;

namespace fintrack.ViewModels;

public class DepositViewModel
{
    [Required]
    [Range(1, 100000)]
    public decimal Amount { get; set; }
}