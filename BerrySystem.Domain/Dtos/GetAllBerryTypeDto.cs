namespace BerrySystem.Domain.Dtos;

public class GetAllBerryTypeDto(Guid id, string type)
{
    public Guid Id { get; set; } = id;
    public string Type { get; set; } = type;
}