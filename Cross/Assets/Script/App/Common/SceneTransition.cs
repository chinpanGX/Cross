using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace App.Common
{
    public class SceneTransition
    {
        private Fade fade = ComponentLocator.Get<Fade>();

        public async Awaitable ChangeScene(string sceneName)
        {
            await fade.FadeIn();
            
        }
    }
}