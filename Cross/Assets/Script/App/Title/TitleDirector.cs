using System;
using App.Common;
using App.Title.Signup;
using Core;
using Core.Director;
using Core.Presenter;
using Core.SaveData;
using Cysharp.Threading.Tasks;
using MasterData;
using Repository.GameTime;
using UnityEngine;
using R3;
using R3.Triggers;
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
                new LoginRefreshTimeData(loginMaster.ToDateTime(DateTime.Now));
            
            saveDataRepository = new PlayerProfileSaveDataLocalRepository(new EncryptedPlayerPrefs());
            gameTimeRepository = new GameTimeLocalRepository(loginRefreshTimeData);
            Push("Title");
            this.UpdateAsObservable().Subscribe(_ => updatablePresenter.Execute()).AddTo(gameObject);
        }

        public async void Push(string name)
        {
            await fade.FadeIn();
            saveDataRepository.ApplyLoginTime();
            gameTimeRepository.Apply(new GameTimeData(saveDataRepository.Get().LastLoginTime));
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