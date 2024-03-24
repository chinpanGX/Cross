using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;

namespace App.Common
{
    internal class Fade : MonoBehaviour
    {
        [SerializeField] private CanvasGroup fadeCanvasGroup;
        [Range(0.1f, 1.0f)]
        [SerializeField] private float durationSecond;
        
        public async UniTask FadeIn()
        {
            transform.SetAsLastSibling();
            await fadeCanvasGroup.DOFade(1f, durationSecond);
        }

        public async UniTask FadeOut()
        {
            await fadeCanvasGroup.DOFade(0f, durationSecond);
        }

        public void BlackOut()
        {
            transform.SetAsLastSibling();
            fadeCanvasGroup.alpha = 1;
        }
    }
}