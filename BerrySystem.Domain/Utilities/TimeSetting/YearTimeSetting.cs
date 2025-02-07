namespace BerrySystem.Domain.Utilities.TimeSetting;

public class YearTimeSetting<T> : TimeSetting<T> where T : class, new()
{
    public override int SelectByType(DateTime dateTime)
    {
        return dateTime.Year;
    }

    public override void TableFormat(ref Dictionary<int, T> table, DateTime dateParameter)
    {
        throw new NotImplementedException();
    }
}