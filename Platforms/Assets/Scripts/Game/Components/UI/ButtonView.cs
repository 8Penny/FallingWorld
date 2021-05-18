using System;
using System.Reflection;
using Game.Managers.UIPoolManager;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Zenject;
using Game.Components.Core;

namespace Game.Components.UI
{
    public class ButtonView<T> : FWView<T> where T : ButtonPresenter
    {
        [SerializeField]
        protected Button _button;
        
        private FWEvent ButtonClick;

        protected virtual void Awake()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            ButtonClick = CreateEvent();
            Bind(ButtonClick, _presenter.OnButtonClick);
        }

        private void OnButtonClick()
        {
            ButtonClick.Invoke();
        }
    }
}