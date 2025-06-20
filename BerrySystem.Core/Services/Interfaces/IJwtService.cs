using BerrySystem.Domain.Entities;

namespace BerrySystem.Core.Services.Interfaces;

public interface IJwtService
{
    public string GenerateJwtToken(Employee employee);
}