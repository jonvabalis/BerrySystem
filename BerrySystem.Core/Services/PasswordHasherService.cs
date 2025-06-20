using BerrySystem.Core.Services.Interfaces;
using Isopoh.Cryptography.Argon2;

namespace BerrySystem.Core.Services;

public class PasswordHasherService : IPasswordHasherService
{
    public string Hash(string password)
    {
        var hash = Argon2.Hash(password);
        return hash;
    }

    public bool Verify(string password, string hash)
    {
        return Argon2.Verify(hash, password);
    }
}