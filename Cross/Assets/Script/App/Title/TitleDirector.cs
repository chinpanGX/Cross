using System;
using App.Common;
using Core;
using Core.Director;
using Core.Presenter;
using MasterData;
using Repository.GameTime;
using UnityEngine;
using R3;
using Script.App.Boot;

namespace App.Title
{
    public class TitleDirector : MonoBehaviour, IDirector
    {
        private UpdatablePresenter updatablePresenter;
        private Fade fade;
        private IGameTimeRepository repository;
        
        private async void Start()
        {
            fade = ComponentLocator.Get<Fade>();
            updatablePresenter = new UpdatablePresenter();
            var loginMaster = await LoginMaster.Load();
            var loginRefreshTimeData =
                new LoginRefreshTimeData(loginMaster.RefreshLoginHour, loginMaster.RefreshLoginMinute, loginMaster.RefreshLoginSecond);
            repository = new GameTimeLocalRepository(loginRefreshTimeData);
            repository.OnRefreshLogin.Subscribe(_ => Debug.Log("日付更新"));
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