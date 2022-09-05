using System;

namespace LibHouse.Infrastructure.Authentication.Extensions.Common
{
    internal static class LongExtensions
    {
        internal static DateTime UnixTimeStampToDateTime(this long unixTimeStamp)
        {
            DateTime dateTimeVal = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();

            return dateTimeVal;
        }
    }
}