using System;
using System.Collections;
using System.Collections.Generic;
using Core.Model;
using Core.SaveData;
using R3;
using Repository.SaveData;
using UnityEngine;
using UnityEngine.PlayerLoop;

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
        private ISaveDataRepository repository;

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
            if (repository.Load())
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