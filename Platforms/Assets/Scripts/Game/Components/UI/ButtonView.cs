using System;
using System.Reflection;
using Game.Managers.UIPoolManager;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Zenject;

namespace Game.Components.UI
{
    public class ButtonView : UIView<ButtonPresenter>
    {
        [SerializeField]
        private Button _button;
        
        private UIEvent ButtonClick;

        private void Awake()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            ButtonClick = CreateEvent();
            _presenter.Bind(ButtonClick, _presenter.OnButtonClick);
        }

        private void OnButtonClick()
        {
            ButtonClick.Invoke();
        }
    }
}