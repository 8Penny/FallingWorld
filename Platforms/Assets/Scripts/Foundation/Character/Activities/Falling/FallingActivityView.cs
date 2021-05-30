using UnityEngine;

namespace Foundation.Activities.Falling {
    public class FallingActivityView : CharacterActivityView{
        
        private static readonly int TriggerID = Animator.StringToHash("PlayFalling");
        public override void OnStart() {
            _animatorController.SetTrigger(TriggerID);
        }

        public override void OnFinish() {
            _animatorController.SetTrigger(StopID);
        }

        public override void OnCancel() {
            _animatorController.SetTrigger(StopID);
        }

        public override void OnReset() {
        }
    }
}