using System;
using System.Collections.Generic;
using Game.Components.Core;
using Game.Managers.UIPoolManager;
using UnityEngine;
using Zenject;

namespace Game.Components.Core
{
    public class FWView<T> : MonoBehaviour where T : FWPresenter
    {
        private List<FWEvent> _uiEvents = new List<FWEvent>();
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

        protected FWEvent CreateEvent()
        {
            FWEvent newEvent = _UIPoolManager.GetUIEvent();
            _uiEvents.Add(newEvent);
            return newEvent;
        }

        protected void Bind(FWEvent ev, Action ac) {
            _presenter.Bind(ev, ac);
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