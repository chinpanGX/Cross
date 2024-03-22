using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Common
{
    public class SceneTransition : MonoBehaviour
    {
        [SerializeField] private Fade fade;
        
        /*
         * 実装機能　async await　
         * フェードイン開始
         * 現在のシーンのアンロード
         * リソースのアンロード
         * 次のシーンをロード
         * フェードアウト
         */
        public async Awaitable ChangeScene(string sceneName)
        {
            await fade.FadeIn();
        }
    }
}