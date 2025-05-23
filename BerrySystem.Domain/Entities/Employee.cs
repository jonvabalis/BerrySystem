﻿namespace BerrySystem.Domain.Entities;

public class Employee : Entity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? Birthday { get; set; }
}