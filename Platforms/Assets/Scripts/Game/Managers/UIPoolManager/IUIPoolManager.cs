using Game.Components.Core;

namespace Game.Managers.UIPoolManager
{
    public interface IUIPoolManager
    {
        FWEvent GetUIEvent();
        void ReturnUIEvent(FWEvent UIevent);
    }
}