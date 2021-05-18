using UnityEngine;

namespace Foundation.Activities
{
    public abstract class CharacterActivityView
    {
        protected static readonly int StopID = Animator.StringToHash("Stop");
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