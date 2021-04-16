using UnityEditor.Animations;

namespace Foundation.Activities
{
    public abstract class CharacterActivityView
    {
        protected AnimatorController _animatorController;
        public void SetUp(AnimatorController animatorController)
        {
            _animatorController = animatorController;
        }
        public abstract void OnStart();
        public abstract void OnFinish();
        public abstract void OnCancel();
        public abstract void OnReset();
    }
}