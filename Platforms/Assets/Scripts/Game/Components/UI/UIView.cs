using System;
using System.Collections.Generic;
using Game.Managers.UIPoolManager;
using UnityEngine;
using Zenject;

namespace Game.Components.UI
{
    public class UIView<T> : MonoBehaviour where T : UIPresenter
    {
        private List<UIEvent> _uiEvents = new List<UIEvent>();
        private IUIPoolManager _UIPoolManager;
        
        protected T _presenter;
        
        [Inject]
        public void Init(IUIPoolManager uiPoolManager)
        {
            _UIPoolManager = uiPoolManager;
        }

        public void SetPresenter(T presenter)
        {
            if (_presenter != null)
            {
                _presenter.Unsubscribe();
            }
            _presenter = presenter;
            presenter.OnViewAttached();
            OnAttached();
        }

        protected UIEvent CreateEvent()
        {
            UIEvent newEvent = _UIPoolManager.GetUIEvent();
            _uiEvents.Add(newEvent);
            return newEvent;
        }
        
        protected virtual void OnDestroy()
        {
            _presenter?.OnViewDestroyed();
            foreach (var e in _uiEvents)
            {
                _UIPoolManager.ReturnUIEvent(e);
            }
            _uiEvents.Clear();
        }

        protected virtual void OnAttached()
        {
            
        }
    }
}