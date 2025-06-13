namespace BerrySystem.Core.Validation;

public class NotFoundException(string entityName, object key)
    : Exception($"{entityName} with identifier '{key}' was not found.");