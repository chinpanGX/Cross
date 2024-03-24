using System;

namespace App.Partial
{
    public static class DateTimeExtension
    {
        public static string ToString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy/MM/dd HH:mm:ss");
        }
    }
}
