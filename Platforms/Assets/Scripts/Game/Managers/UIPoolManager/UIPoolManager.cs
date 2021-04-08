using System.Collections.Generic;
using Foundation;
using Game.Components.UI;

namespace Game.Managers.UIPoolManager
{
    public class UIPoolManager : AbstractService<IUIPoolManager>, IUIPoolManager
    {
        private List<UIEvent> _uiEvents = new List<UIEvent>();
        private int _lastFreeUIEventIndex = -1;
        public UIEvent GetUIEvent()
        {
            if (_lastFreeUIEventIndex <= _uiEvents.Count - 1)
            {
                return new UIEvent();
            }

            return _uiEvents[_lastFreeUIEventIndex--];
        }

        public void ReturnUIEvent(UIEvent ev)
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