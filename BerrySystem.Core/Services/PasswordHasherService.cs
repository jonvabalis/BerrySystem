using BerrySystem.Core.Services.Interfaces;

namespace BerrySystem.Core.Services;

public class PasswordHasherService : IPasswordHasherService
{
    public string? Hash(string password)
    {
        var hash = BCrypt.Net.BCrypt.HashPassword(password, 11);
        return hash;
    }

    public bool Verify(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}