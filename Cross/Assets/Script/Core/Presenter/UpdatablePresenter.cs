#nullable enable

namespace Core.Presenter
{
    public class UpdatablePresenter
    {
        private IPresenter? presenter = null;
        private IPresenter? request = null;

        public void Execute()
        {
            if (request != null)
            {
                presenter?.Dispose();
                presenter = request;
                request = null!;
            }

            presenter?.Execute();
        }

        public void Set(IPresenter presenter)
        {
            request = presenter;
        }
    }
}