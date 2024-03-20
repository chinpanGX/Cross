using R3;

namespace Repository.GameTime
{
    public interface IGameTimeRepository
    {
        void Apply(GameTimeData source);
        Observable<Unit> OnRefreshLogin { get; }
    }
}