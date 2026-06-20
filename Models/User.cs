namespace fintrack.Models;
using System.ComponentModel.DataAnnotations;
public class User
{
    public int Id {get; set;}

    [Required]
    public string Name{get; set;}=null!;

    [Required, EmailAddress]
    public string Email{get; set;}=null!;

        [Required, MinLength(8)]
    public string Password{get; set;}=null!;

    public decimal Balance{get;set;}=0;
}
