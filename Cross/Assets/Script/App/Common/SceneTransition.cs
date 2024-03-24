using System;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Common
{
    public static class SceneName
    {
        public static readonly string Core = "CoreScene";
        public static readonly string Title = "TitleScene";
        public static readonly string Home = "Home";
    }
    
    public class SceneTransition
    {
        private readonly Fade fade = ComponentLocator.Get<Fade>();

        public async void ChangeScene(string currentSceneName, string nextSceneName)
        {
            if (currentSceneName == SceneName.Core || nextSceneName == SceneName.Core)
            {
                throw new ArgumentException($"不正なシーン名：{SceneName.Core}が指定されています。");
            }
            
            await fade.FadeIn();
            await SceneManager.UnloadSceneAsync(currentSceneName);
            await Resources.UnloadUnusedAssets();
            await SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);
            await fade.FadeOut();
        }
    }
}