using UnityEngine;

namespace Script.App.Common
{
    public static class CanvasGroupExtension
    {
        public static void SetInteractable(this CanvasGroup canvasGroup, bool flag)
        {
            if (canvasGroup != null)
            {
                canvasGroup.interactable = flag;   
            }
        }
    }
}