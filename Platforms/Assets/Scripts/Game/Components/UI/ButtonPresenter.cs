using System;
using Foundation;
using Game.Managers.UIPoolManager;

namespace Game.Components.UI
{
    public class ButtonPresenter : UIPresenter
    {
        public ButtonPresenter(IUIPoolManager uiPoolManager) : base(uiPoolManager)
        {
        }

        public override void OnViewAttached()
        {
        }

        public void OnButtonClick()
        {
            DebugOnly.Message("Click");
        }
    }
}