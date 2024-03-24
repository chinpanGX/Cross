using System;

namespace Repository.GameTime
{
    /// <summary>
    /// ログイン更新時間
    /// </summary>
    public readonly struct LoginRefreshTimeData
    {
        public readonly DateTime RefreshTime;
        //
        // public LoginRefreshTimeData(int hour, int minute, int second)
        // {
        //     var time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, second);
        //
        //     if (time.Subtract(DateTime.Now) <= TimeSpan.Zero)
        //     {
        //         time.AddDays(1);
        //     }
        //     
        //     if (time == DateTime.MinValue)
        //     {
        //         throw new ArgumentException($"DateTimeがデフォルト値です。{time}");
        //     }
        //     RefreshTime = time;
        // }

        public LoginRefreshTimeData(DateTime loginRefreshTime)
        {
            var time = loginRefreshTime;
            if (time == DateTime.MinValue)
            {
                throw new ArgumentException($"DateTimeがデフォルト値です。{time}");
            }
            RefreshTime = time;
        }

        public DateTime AddDays()
        {
            return RefreshTime.AddDays(1);
        }
    }

}