using System;
using R3;

namespace Repository.GameTime
{
    public class GameTimeLocalRepository : IGameTimeRepository, IDisposable
    {
        private readonly Subject<Unit> changeDate = new();
        private readonly LoginRefreshTimeData loginRefreshTime;

        public GameTimeData TimeData { get; private set; }
        public Observable<Unit> OnRefreshLogin => changeDate;

        public GameTimeLocalRepository(LoginRefreshTimeData loginRefreshTime)
        {
            this.loginRefreshTime = loginRefreshTime;
        }

        public void Apply(GameTimeData source)
        {
            TimeData = source;

            if (loginRefreshTime.RefreshTime.Subtract(TimeData.DateTime) <= TimeSpan.Zero)
            {
                changeDate.OnNext(Unit.Default);
            }
        }
        public void Dispose()
        {
            changeDate?.Dispose();
        }
    }
}