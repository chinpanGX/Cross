#nullable enable
using System;
using Common.UI;
using UnityEngine;
using TMPro;
using R3;

public class UITextField : MonoBehaviour, IDisposable
{
    [SerializeField] private UIText? title;
    [SerializeField] private TMP_InputField? inputField;
    [SerializeField] private UIButton? button;
    private readonly CompositeDisposable disposable= new ();

    public UIText? Title => title;
    public UIButton? Button => button;

    public void Setup(Action<string> onClickButton)
    {
        button.SetInteractableSafe(false);
        
        if (inputField != null)
        {
            inputField.onValueChanged.AsObservable().Subscribe(text =>
            {
                button.SetInteractableSafe(!string.IsNullOrEmpty(text));
            }).AddTo(disposable);
        }

        string GetTextSafe()
        {
            if (inputField != null)
            {
                return inputField.text;
            }
            return "";
        }
        button.SetupSafe(() => onClickButton.Invoke(GetTextSafe()));
    }

    public void SetTitle(string text)
    {
        title.SetTextSafe(text);
    }

    public void SetButtonLabel(string text)
    {
        if (button != null)
        {
            button.SetLabel(text);   
        }
    }
    
    public void Dispose()
    {
        if (button != null)
        {
            button.Dispose();   
        }
        if(disposable.IsDisposed)
            return;
        disposable.Dispose();
    }
}
