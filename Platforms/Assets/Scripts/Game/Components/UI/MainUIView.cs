using UnityEngine;

namespace Game.Components.UI {
    public class MainUIView : MonoBehaviour {
        [SerializeField]
        private ActivityButtonView _activityButtonView;

        public ActivityButtonView ActivityButtonView => _activityButtonView;
    }
}