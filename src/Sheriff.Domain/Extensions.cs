using System;

namespace Sheriff
{
    public static class StringExtensions
    {
        public static string Format(this string format, params object[] args)
        {
            return String.Format(format, args);
        }
    }

    static class DateTimeExtensions
    {
        public static bool IsInPast(this DateTime dateTime)
        {
            return dateTime < DateTime.Now;
        }

        public static bool IsInFuture(this DateTime dateTime)
        {
            return dateTime > DateTime.Now;
        }
    }
}