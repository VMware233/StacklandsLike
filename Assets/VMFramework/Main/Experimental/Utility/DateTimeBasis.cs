using System;
using System.Runtime.CompilerServices;

namespace Basis
{
    [Flags]
    public enum TimeType
    {
        Year = 1,
        Mouth = 1 << 1,
        Day = 1 << 2,
        Hour = 1 << 3,
        Minute = 1 << 4,
        Second = 1 << 5,
        Millisecond = 1 << 6
    }

    public static class DateTimeFunc
    {
        public static DateTime oneYear = new(1, 0, 0, 0, 0, 0);
        public static DateTime oneMonth = new(0, 1, 0, 0, 0, 0);
        public static DateTime oneDay = new(0, 0, 1, 0, 0, 0, 0);
        public static DateTime oneHour = new(0, 0, 0, 1, 0, 0);
        public static DateTime oneMinute = new(0, 0, 0, 0, 1, 0);
        public static DateTime oneSecond = new(0, 0, 0, 0, 0, 1);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Get(this DateTime date, TimeType timeType) =>
            timeType switch
            {
                TimeType.Year => date.Year,
                TimeType.Mouth => date.Month,
                TimeType.Day => date.Day,
                TimeType.Hour => date.Hour,
                TimeType.Minute => date.Minute,
                TimeType.Second => date.Second,
                TimeType.Millisecond => date.Millisecond,
                _ => throw new ArgumentOutOfRangeException(nameof(timeType), timeType, null)
            };
    }
}

