using UnityEngine;

namespace Foundation.Activities.Idle {
    public class MovementActivityView : CharacterActivityView {
        private static readonly int TriggerID = Animator.StringToHash("PlayMovement");
        private static readonly int SpeedID = Animator.StringToHash("Speed");
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

        public void UpdateSpeed(float value) {
            _animatorController.SetFloat(SpeedID, value);
        }
    }
}