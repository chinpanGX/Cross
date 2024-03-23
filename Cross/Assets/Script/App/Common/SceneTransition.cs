using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Common
{
    public static class SceneName
    {
        public static readonly string Core = "CoreScene";
        public static readonly string Title = "TitleScene";
    }
    
    public class SceneTransition
    {
        private Fade fade = ComponentLocator.Get<Fade>();

        public async void ChangeScene(string sceneName)
        {
            await fade.FadeIn();
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            await fade.FadeOut();
        }
    }
}