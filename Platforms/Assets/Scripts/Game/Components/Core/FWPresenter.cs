using System;
using System.Collections.Generic;
using Foundation;
using Game.Components.Core;
using Game.Managers.UIPoolManager;
using Zenject;

namespace Game.Components.Core {
    public class FWPresenter : IDisposable {
        ObserverHandleManager observers;
        private List<FWEvent> _uiEvents = new List<FWEvent>();
        private IUIPoolManager _UIPoolManager;

        [Inject]
        public void Init(IUIPoolManager uiPoolManager) {
            _UIPoolManager = uiPoolManager;
            OnInit();
        }
        
        protected virtual void OnInit(){}

        public virtual void OnViewAttached() {
        }

        public void Bind(FWEvent ev, Action ac) {
            ev.Add(ac);
            _uiEvents.Add(ev);
        }

        protected FWEvent CreateEvent() {
            FWEvent newEvent = _UIPoolManager.GetUIEvent();
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