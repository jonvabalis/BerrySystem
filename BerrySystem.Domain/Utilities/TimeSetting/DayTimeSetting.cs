namespace BerrySystem.Domain.Utilities.TimeSetting;

public class DayTimeSetting<T> : TimeSetting<T> where T : class, new()
{
    public override int SelectByType(DateTime dateTime)
    {
        return dateTime.Day;
    }

    public override void TableFormat(ref Dictionary<int, T> table, DateTime dateParameter)
    {
        var totalDays = DateTime.DaysInMonth(dateParameter.Year, dateParameter.Month);

        for (int i = 1; i <= totalDays; i++)
        {
            table.TryAdd(i, new T());
        }
        
        table = table.OrderBy(e => e.Key)
            .ToDictionary(e => e.Key, e => e.Value);
    }
}