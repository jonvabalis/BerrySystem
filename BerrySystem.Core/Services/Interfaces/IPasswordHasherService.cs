namespace BerrySystem.Core.Services.Interfaces;

public interface IPasswordHasherService
{
    public string? Hash(string password);

    public bool Verify(string password, string hash);
}