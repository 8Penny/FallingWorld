using UnityEngine;
using Zenject;
using IInstantiator = Game.Managers.Instantiator.IInstantiator;

namespace Game.Components.UI {
    public class MainUIController : MonoBehaviour {
        [SerializeField]
        private MainUIView _mainUIView;
        
        private IInstantiator _instantiator;
        
        [Inject]
        private void Init(IInstantiator instantiator) {
            _instantiator = instantiator;
        }

        private void Awake() {
            ActivityButtonPresenter activityButtonPresenter = _instantiator.Instantiate<ActivityButtonPresenter>();
            TimerPresenter timerPresenter = _instantiator.Instantiate<TimerPresenter>();
            
            _mainUIView.ActivityButtonView.SetPresenter(activityButtonPresenter);
            _mainUIView.TimerView.SetPresenter(timerPresenter);
        }
    }
}