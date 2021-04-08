using System;
using System.Collections.Generic;
using Foundation;
using Game.Managers.UIPoolManager;
using Zenject;

namespace Game.Components.UI {
    public class UIPresenter : IDisposable {
        ObserverHandleManager observers;
        private List<UIEvent> _uiEvents = new List<UIEvent>();
        private IUIPoolManager _UIPoolManager;

        [Inject]
        public void Init(IUIPoolManager uiPoolManager) {
            _UIPoolManager = uiPoolManager;
        }

        public virtual void OnViewAttached() {
        }

        public void Bind(UIEvent ev, Action ac) {
            ev.Add(ac);
            _uiEvents.Add(ev);
        }

        protected UIEvent CreateEvent() {
            UIEvent newEvent = _UIPoolManager.GetUIEvent();
            _uiEvents.Add(newEvent);
            return newEvent;
        }

        public void OnViewDestroyed() {
            UnsubscribeFromCurrentActions();
        }

        public void Unsubscribe() {
            UnsubscribeFromCurrentActions();
        }


        private void UnsubscribeFromCurrentActions() {
            for (int i = 0; i < _uiEvents.Count; i++) {
                var ev = _uiEvents[i];
                ev.Clear();
                _UIPoolManager.ReturnUIEvent(ev);
            }

            _uiEvents.Clear();
        }


        protected void Observe<O>(IObserverList<O> observable)
            where O : class {
            Observers.Observe(observable, this as O);
        }

        public void Dispose() {
            UnsubscribeFromCurrentActions();
            observers?.Clear();
        }

        protected ObserverHandleManager Observers {
            get {
                if (observers == null) {
                    observers = new ObserverHandleManager();
                }

                return observers;
            }
        }
    }
}