namespace BerrySystem.Domain.Utilities.TimeSetting;

public class MonthTimeSetting<T> : TimeSetting<T> where T : class, new()
{
    public override int SelectByType(DateTime dateTime)
    {
        return dateTime.Month;
    }

    public override void TableFormat(ref Dictionary<int, T> table, DateTime dateParameter)
    {
        throw new NotImplementedException();
    }
}