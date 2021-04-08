using Game.Components.UI;

namespace Game.Managers.UIPoolManager
{
    public interface IUIPoolManager
    {
        UIEvent GetUIEvent();
        void ReturnUIEvent(UIEvent UIevent);
    }
}