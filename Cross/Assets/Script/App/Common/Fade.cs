using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;

namespace App.Common
{
    internal class Fade : MonoBehaviour
    {
        [SerializeField] private CanvasGroup fadeCanvasGroup;
        
        /*
         * 実装機能
         * フェードイン　async await
         * フェードアウト async await
         */

        public async Awaitable FadeIn()
        {
            await fadeCanvasGroup.DOFade(1f, 1f);
        }

        public async Awaitable FadeOut()
        {
            await fadeCanvasGroup.DOFade(0f, 1f);
        }
    }
}