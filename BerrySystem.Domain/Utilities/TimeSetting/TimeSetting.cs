namespace BerrySystem.Domain.Utilities.TimeSetting;

public abstract class TimeSetting
{
    public abstract int SelectByType(DateTime dateTime);
    public abstract Dictionary<int, T> TableFormat<T>(Dictionary<int, T> table, DateTime dateParameter) where T : class, new();
}