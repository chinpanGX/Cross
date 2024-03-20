using System;

namespace Core.Presenter
{
    public interface IPresenter : IDisposable
    {
        void Execute();
    }
}