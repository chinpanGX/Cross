using Core.Model;
using R3;
using Repository.SaveData;

namespace App.Title
{
    public class TitleModel : IModel
    {
        public enum TransitionState
        {
            Signup,
            Home
        }
        
        private readonly Subject<TransitionState> transitionState = new();
        private readonly ISaveDataRepository repository;

        public Observable<TransitionState> OnTransitionState => transitionState;

        public TitleModel(ISaveDataRepository repository)
        {
            this.repository = repository;
        }
        
        public void Dispose()
        {
            transitionState.OnCompleted();
            transitionState.Dispose();
        }

        public void Execute()
        {
            if (repository.UserRegistered)
            {
                transitionState.OnNext(TransitionState.Home);
            }
            else
            {
                transitionState.OnNext(TransitionState.Signup);
            }
        }
    }
}