namespace BerrySystem.Domain.Dtos;

public class AccessTokenUserIdDto
{
    public Guid EmployeeId { get; set; }
    public required string AccessToken { get; set; }
}