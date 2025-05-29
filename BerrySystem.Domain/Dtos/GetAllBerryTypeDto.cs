namespace BerrySystem.Domain.Dtos;

public class GetAllBerryTypeDto(Guid id, string name)
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
}