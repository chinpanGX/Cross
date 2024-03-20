#nullable enable
using TMPro;
using UnityEngine;

namespace Common.UI
{
    public class UIText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI? text;
        public TextMeshProUGUI? Text => text;

        /// <summary>
        /// テキストをセット
        /// </summary>
        public void SetText(string value)
        {
            if (text == null) return;

            if (text.text != value)
            {
                text.text = value;
            }
        }

        /// <summary>
        /// 色をセット
        /// </summary>
        public void SetColor(Color color)
        {
            if (text == null) return;
            text.color = color;
        }
    }

    /// <summary>
    /// 拡張機能
    /// </summary>
    public static class UITextExtension
    {
        public static void SetTextSafe(this UIText? obj, string value)
        {
            if (obj != null)
            {
                obj.SetText(value);
            }
        }

        public static void SetColorSafe(this UIText? obj, Color color)
        {
            if (obj != null)
            {
                obj.SetColor(color);
            }
        }
    }
}