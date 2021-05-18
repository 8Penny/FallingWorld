using UnityEngine;

namespace Game.Components.UI {
    public class MainUIView : MonoBehaviour {
        [SerializeField]
        private ActivityButtonView _activityButtonView;
        [SerializeField]
        private TimerView _timerView;

        public ActivityButtonView ActivityButtonView => _activityButtonView;
        public TimerView TimerView => _timerView;
    }
}