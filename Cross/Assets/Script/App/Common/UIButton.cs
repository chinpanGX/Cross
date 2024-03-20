#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using R3;

namespace Common.UI
{
    public class UIButton : MonoBehaviour, IDisposable
    {
        [SerializeField] private UIText? label;
        [SerializeField] private Button? button;
        private readonly CompositeDisposable disposable = new ();

        public UIText? Label => label;
        public Button? Button => button;
        
        /// <summary>
        /// 後始末
        /// </summary>
        public void Dispose()
        {
            if(disposable.IsDisposed) 
                return;
            disposable.Dispose();
        }

        /// <summary>
        /// セットアップ
        /// </summary>
        /// <param name="call"> アクション </param>
        public void Setup(Action call)
        {
            if (button != null)
            {
                button.onClick.AsObservable().Subscribe(_ => call.Invoke()).AddTo(disposable);
            }
        }
        
        /// <summary>
        /// ラベルの設定
        /// </summary>
        /// <param name="text"></param>
        public void SetLabel(string text)
        {
            Label.SetTextSafe(text); 
        }
        
        /// <summary>
        /// ボタンの入力可否を設定
        /// </summary>
        /// <param name="flag"></param>
        public void SetInteractable(bool flag)
        {
            if (button != null)
            {
                button.interactable = flag;
            }
        }
        
        /// <summary>
        /// 色の設定
        /// </summary>
        /// <param name="color"></param>
        public void SetColor(Color color)
        {
            if (button != null)
            {
                button.image.color = color;   
            }
        }
    }

    /// <summary>
    /// 拡張機能
    /// </summary>
    public static class UIButtonExtensions
    {
        public static void SetupSafe(this UIButton? obj, Action call) 
        {
            if (obj != null)
            {
                obj.Setup(call);
            }
        }

        public static void SetInteractableSafe(this UIButton? obj, bool flag)
        {
            if (obj != null)
            {
                obj.SetInteractable(flag);
            }
        }
    }
}