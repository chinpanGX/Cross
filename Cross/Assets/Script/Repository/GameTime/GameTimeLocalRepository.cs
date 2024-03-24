using System;
using NUnit.Framework;
using R3;

namespace Repository.GameTime
{
    public class GameTimeLocalRepository : IGameTimeRepository, IDisposable
    {
        private readonly Subject<Unit> changeDate = new();
        private LoginRefreshTimeData loginRefreshTime;
        private GameTimeData lastLoginTime;
        
        public Observable<Unit> OnRefreshLogin => changeDate;

        public GameTimeLocalRepository(LoginRefreshTimeData loginRefreshTime)
        {
            this.loginRefreshTime = loginRefreshTime;
        }
        
        public void Apply(GameTimeData source)
        {
            lastLoginTime = source;
            
            if (loginRefreshTime.RefreshTime.Subtract(lastLoginTime.DateTime) <= TimeSpan.Zero)
            {
                var tmp = new LoginRefreshTimeData(loginRefreshTime.AddDays());
                loginRefreshTime = tmp;
                changeDate.OnNext(Unit.Default);
            }
        }
        public void Dispose()
        {
            changeDate?.Dispose();
        }
    }
}