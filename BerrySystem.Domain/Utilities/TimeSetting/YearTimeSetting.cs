namespace BerrySystem.Domain.Utilities.TimeSetting;

public class YearTimeSetting : TimeSetting
{
    public override int SelectByType(DateTime dateTime)
    {
        return dateTime.Year;
    }

    public override Dictionary<int, T> TableFormat<T>(Dictionary<int, T> table, DateTime dateParameter)
    {
        var oldestYear = table.Min(e => e.Key);
        var newestYear = table.Max(e => e.Key);

        for (int i = newestYear; i <= oldestYear; i++)
        {
            table.TryAdd(i, new T());
        }

        return table.OrderBy(e => e.Key)
            .ToDictionary(e => e.Key, e => e.Value);
    }
}