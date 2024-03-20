using System;

namespace Repository.GameTime
{
    /// <summary>
    /// ログイン更新時間
    /// </summary>
    public readonly struct LoginRefreshTimeData
    {
        public readonly DateTime RefreshTime;
        
        public LoginRefreshTimeData(int hour, int minute, int second)
        {
            var time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, second);
            if (time == DateTime.MinValue)
            {
                throw new ArgumentException($"DateTimeがデフォルト値です。{time}");
            }
            RefreshTime = time;
        }
    }

}