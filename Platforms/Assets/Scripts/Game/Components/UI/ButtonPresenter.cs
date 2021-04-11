using System;
using Foundation;
using Game.Components.Core;

namespace Game.Components.UI
{
    public class ButtonPresenter : FWPresenter
    {
        public override void OnViewAttached()
        {
        }

        public virtual void OnButtonClick()
        {
            DebugOnly.Message("Click");
        }
    }
}