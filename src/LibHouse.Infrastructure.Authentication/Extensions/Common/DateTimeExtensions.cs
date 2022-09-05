using System;

namespace LibHouse.Infrastructure.Authentication.Extensions.Common
{
    public static class DateTimeExtensions
    {
        public static long ToUnixEpochDate(this DateTime date)
        {
            return (long) Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        }
    }
}