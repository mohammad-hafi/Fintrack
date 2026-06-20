namespace fintrack.Services;
using System.Security.Cryptography;
using System.Text;
public class PasswordHash
{
    public string Hash(String password)
    {
        using var pass = SHA256.Create();

        var bytes = pass.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }
    public bool Verify(String password, String hash)
    {
        return Hash(password)==hash;
    }
}
