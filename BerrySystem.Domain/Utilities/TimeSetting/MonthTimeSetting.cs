namespace BerrySystem.Domain.Utilities.TimeSetting;

public class MonthTimeSetting : TimeSetting
{
    public override int SelectByType(DateTime dateTime)
    {
        return dateTime.Month;
    }

    public override Dictionary<int, T> TableFormat<T>(Dictionary<int, T> table, DateTime dateParameter)
    {
        var totalMonths = 12;

        for (int i = 1; i <= totalMonths; i++)
        {
            table.TryAdd(i, new T());
        }

        return table.OrderBy(e => e.Key)
            .ToDictionary(e => e.Key, e => e.Value);
    }
}