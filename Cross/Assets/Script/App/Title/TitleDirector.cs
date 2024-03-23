using System;
using App.Common;
using Core.Director;
using Core.Presenter;
using MasterData;
using Repository.GameTime;
using UnityEngine;
using R3;

namespace App.Title
{
    public class TitleDirector : MonoBehaviour, IDirector
    {
        private UpdatablePresenter updatablePresenter;
        private Fade fade;
        private IGameTimeRepository repository;

        private void Start()
        {
            updatablePresenter = new UpdatablePresenter();
            var loginMaster = LoginMaster.Load().GetAwaiter().GetResult();
            var loginRefreshTimeData =
                new LoginRefreshTimeData(loginMaster.RefreshLoginHour, loginMaster.RefreshLoginMinute, loginMaster.RefreshLoginSecond);
            repository = new GameTimeLocalRepository(loginRefreshTimeData);
            repository.OnRefreshLogin.Subscribe(_ => Debug.Log("日付更新"));
            Push("");
        }

        public async void Push(string name)
        {
            await fade.FadeIn();
            
            repository.Apply(new GameTimeData(DateTime.Now));
            
            IPresenter request = name switch
            {
                _ => null!
            };
            updatablePresenter.Set(request);
            await fade.FadeOut(); 
        }
    }


}