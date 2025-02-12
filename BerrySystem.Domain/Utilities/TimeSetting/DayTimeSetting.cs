namespace BerrySystem.Domain.Utilities.TimeSetting;

public class DayTimeSetting : TimeSetting
{
    public override int SelectByType(DateTime dateTime)
    {
        return dateTime.Day;
    }

    public override Dictionary<int, T> TableFormat<T>(Dictionary<int, T> table, DateTime dateParameter)
    {
        var totalDays = DateTime.DaysInMonth(dateParameter.Year, dateParameter.Month);

        for (int i = 1; i <= totalDays; i++)
        {
            table.TryAdd(i, new T());
        }

        return table.OrderBy(e => e.Key)
            .ToDictionary(e => e.Key, e => e.Value);
    }
}