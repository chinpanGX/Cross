#nullable enable
using Core;
using Core.Presenter;
using R3;

namespace App.Title.Signup
{
    public class SignupPresenter : IPresenter
    {
        private TitleDirector Director { get; set; }
        private SignupModel Model { get; set; }
        private SignupView View { get; set; }
        private StateMachine<SignupPresenter> StateMachine { get; set; }

        public SignupPresenter(TitleDirector director, SignupModel model, SignupView view)
        {
            Director = director;
            Model = model;
            View = view;
            StateMachine = new StateMachine<SignupPresenter>(this);
            StateMachine.Change<StateInit>();
        }
        
        public void Dispose()
        {
            Director = null!;
            View.Pop();
            View = null!;
            Model.Dispose();
            Model = null!;
            StateMachine.Dispose();
            StateMachine = null!;
        }

        public void Execute()
        {
            StateMachine.Execute();
        }

        class StateInit : StateMachine<SignupPresenter>.State
        {
            readonly CancellationDisposable cancellationDisposable = new ();

            public override void Begin(SignupPresenter owner)
            {
                var view = owner.View;
                view.Push();
            }
        }
    }
}