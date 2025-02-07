namespace BerrySystem.Domain.Utilities.TimeSetting;

public abstract class TimeSetting<T> where T : class, new()
{
    public abstract int SelectByType(DateTime dateTime);
    public abstract void TableFormat(ref Dictionary<int, T> table, DateTime dateParameter);
}