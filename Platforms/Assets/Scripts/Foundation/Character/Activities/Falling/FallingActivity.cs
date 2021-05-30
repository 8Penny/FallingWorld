namespace Foundation.Activities.Falling {
    public class FallingActivity : CharacterActivity, IOnUpdate {
        public override ActivityType ActivityType => ActivityType.Falling;

        public void Do(float timeDelta) {
        }

        public override void Reset() {
            base.Reset();
            Clear();
        }
    }
}