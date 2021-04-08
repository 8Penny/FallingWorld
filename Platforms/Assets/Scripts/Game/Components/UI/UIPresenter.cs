using System;
using System.Collections.Generic;
using Game.Managers.UIPoolManager;

namespace Game.Components.UI
{
    public class UIPresenter
    {
        private List<UIEvent> _uiEvents = new List<UIEvent>();
        private IUIPoolManager _UIPoolManager;

        public UIPresenter(IUIPoolManager uiPoolManager)
        {
            _UIPoolManager = uiPoolManager;
        }


        public virtual void OnViewAttached()
        {
        }

        public void Bind(UIEvent ev, Action ac)
        {
            ev.Add(ac);
            _uiEvents.Add(ev);
        }
        protected UIEvent CreateEvent()
        {
            UIEvent newEvent = _UIPoolManager.GetUIEvent();
            _uiEvents.Add(newEvent);
            return newEvent;
        }

        public void OnViewDestroyed()
        {
            UnsubscribeFromCurrentActions();
        }

        public void Unsubscribe()
        {
            UnsubscribeFromCurrentActions();
        }

        
        private void UnsubscribeFromCurrentActions()
        {
            for (int i = 0; i < _uiEvents.Count; i++)
            {
                var ev = _uiEvents[i];
                ev.Clear();
                _UIPoolManager.ReturnUIEvent(ev);
            }
            _uiEvents.Clear();
        }
    }
}