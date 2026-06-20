namespace fintrack.Models;

public class Transaction
{
    public int Id{get;set;}

    public int SenderId{get;set;}
    public User Sender{get;set;}=null!;
    public int ReceiverId{get;set;}
    public User Receiver{get;set;}=null!;

    public decimal Amount{get;set;}

    public DateTime Date{get;set;}=DateTime.UtcNow;
}
