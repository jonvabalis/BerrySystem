using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BerrySystem.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace BerrySystem.Core.Services.Interfaces;

public class JwtService(IConfiguration configuration) : IJwtService
{
    public string GenerateJwtToken(Employee employee)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, employee.Id.ToString()),
            new(ClaimTypes.Name, employee.Email ?? employee.Username!)
        };
        claims.AddRange(employee.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration.GetValue<string>("JwtSettings:SecretToken")!));

        var tokenDescriptor = new JwtSecurityToken(
            issuer: configuration.GetValue<string>("JwtSettings:Issuer"),
            audience: configuration.GetValue<string>("JwtSettings:Audience"),
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("JwtSettings:ExpiryInMinutes")),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha512)
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}