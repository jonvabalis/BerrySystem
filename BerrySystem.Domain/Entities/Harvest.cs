﻿namespace BerrySystem.Domain.Entities;

public class Harvest : Entity
{
    public double Kilograms { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime? EventTime { get; set; }
    public BerryType BerryType { get; set; }
    public BerryKind? BerryKind { get; set; }
}