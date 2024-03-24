using System;
using App.Common;
using App.Title.Signup;
using Core;
using Core.Director;
using Core.Presenter;
using Core.SaveData;
using MasterData;
using Repository.GameTime;
using UnityEngine;
using R3;
using Repository.SaveData;

namespace App.Title
{
    public class TitleDirector : MonoBehaviour, IDirector
    {
        private UpdatablePresenter updatablePresenter;
        private Fade fade;
        private IGameTimeRepository gameTimeRepository;
        private ISaveDataRepository saveDataRepository;
        
        private async void Start()
        {
            fade = ComponentLocator.Get<Fade>();
            updatablePresenter = new UpdatablePresenter();
            var loginMaster = await LoginMaster.Load();
            var loginRefreshTimeData =
                new LoginRefreshTimeData(loginMaster.RefreshLoginHour, loginMaster.RefreshLoginMinute, loginMaster.RefreshLoginSecond);
            gameTimeRepository = new GameTimeLocalRepository(loginRefreshTimeData);
            gameTimeRepository.OnRefreshLogin.Subscribe(_ => Debug.Log("日付更新"));
            saveDataRepository = new PlayerProfileSaveDataLocalRepository(new EncryptedPlayerPrefs());
            Push("Title");
        }

        public async void Push(string name)
        {
            await fade.FadeIn();
            gameTimeRepository.Apply(new GameTimeData(DateTime.Now));
            
            IPresenter request = name switch
            {
                "Title" => new TitlePresenter(this, new TitleModel(saveDataRepository), TitleView.Create()),
                "Signup" => new SignupPresenter(this, new SignupModel(saveDataRepository), SignupView.Create()),
                _ => null!
            };
            updatablePresenter.Set(request);
            await fade.FadeOut(); 
        }
    }


}