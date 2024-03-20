using System;

namespace App.Partial
{
    public static class DateTimeExtension
    {
        public static string ToShortString(this DateTime dateTime)
        {
            return dateTime.ToShortDateString() + dateTime.ToShortTimeString();
        }
    }
}
