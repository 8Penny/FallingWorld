using System.Collections.Generic;
using Foundation;
using Game.Components.Core;

namespace Game.Managers.UIPoolManager
{
    public class UIPoolManager : AbstractService<IUIPoolManager>, IUIPoolManager
    {
        private List<FWEvent> _uiEvents = new List<FWEvent>();
        private int _lastFreeUIEventIndex = -1;
        public FWEvent GetUIEvent()
        {
            if (_lastFreeUIEventIndex <= _uiEvents.Count - 1)
            {
                return new FWEvent();
            }

            return _uiEvents[_lastFreeUIEventIndex--];
        }

        public void ReturnUIEvent(FWEvent ev)
        {
            if (_lastFreeUIEventIndex < _uiEvents.Count - 1)
            {
                _uiEvents[++_lastFreeUIEventIndex] = ev;
                return;
            }
            _uiEvents.Add(ev);
            _lastFreeUIEventIndex++;
        }
    }
}