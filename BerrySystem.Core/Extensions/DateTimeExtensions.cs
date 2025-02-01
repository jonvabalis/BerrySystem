using BerrySystem.Domain.Types;

namespace BerrySystem.Core.Extensions;

public static class DateTimeExtensions
{
    public static Func<DateTime, int> TimeSettingSelector(TimeSettingType timeSetting)
    {
        return timeSetting switch
        {
            TimeSettingType.Year => date => date.Year,
            TimeSettingType.Month => date => date.Month,
            TimeSettingType.Day => date => date.Day,
            _ => throw new ArgumentException("Invalid time setting: ", nameof(timeSetting))
        };
    }
}