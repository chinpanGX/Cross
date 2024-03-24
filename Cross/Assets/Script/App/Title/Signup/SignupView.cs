#nullable enable
using System;
using Common.UI;
using Core;
using Core.View;
using Script.App.Common;
using UnityEngine;
using Screen = Core.View.Screen;

namespace App.Title.Signup
{
    public class SignupView : MonoBehaviour, IView
    {
        [SerializeField] private Canvas? canvas;
        [SerializeField] private CanvasGroup? canvasGroup;
        [SerializeField] private UITextField? textField;
        private static Screen Screen => ComponentLocator.Get<Screen>();
        public Canvas? Canvas => canvas;

        public static SignupView Create()
        {
            return Instantiate(Resources.Load<SignupView>("View/SignupView"));
        }
        
        public void Push()
        {
            Screen.Push(this);
        }
        public void Pop()
        {
            Screen.Pop();
        }
        
        public void Open()
        {
            gameObject.SetActive(true);
            canvasGroup.SetInteractable(true);
        }
        
        public void Close()
        {
            Destroy(gameObject);
        }

        public void OnRegisterUser(Action<string> onResisterUser)
        {
            if (textField != null)
            {
                textField.Setup(onResisterUser);    
            }
        }
    }
}