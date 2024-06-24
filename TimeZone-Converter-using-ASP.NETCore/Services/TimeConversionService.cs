using System;
using TimeZoneConverter;


namespace TimeZone_Converter_using_ASP.NETCore.Services
{
    public class TimeConversionService
    {
        public DateTime ConvertTime(DateTime sourceDateTime, string sourceTimeZone, string targetTimeZone)
        {
            var sourceTimeZoneInfo = TZConvert.GetTimeZoneInfo(sourceTimeZone);
            var targetTimeZoneInfo = TZConvert.GetTimeZoneInfo(targetTimeZone);

            var sourceUtcTime = TimeZoneInfo.ConvertTimeToUtc(sourceDateTime, sourceTimeZoneInfo);
            var targetTime = TimeZoneInfo.ConvertTimeFromUtc(sourceUtcTime, targetTimeZoneInfo);

            return targetTime;
        }
    }
}
