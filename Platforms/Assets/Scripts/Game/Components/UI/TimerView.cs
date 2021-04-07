using Foundation;
using Game.Managers;
using Game.Managers.PhaseManagers;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Components.UI {
    public class TimerView : AbstractBehaviour, IOnUpdate, IOnPhaseCompleted, IOnPhaseStarted {
        [SerializeField]
        private GameObject _mainGameObject;

        [SerializeField]
        private TMP_Text _text;

        private int _timeLeft;
        
        private ISceneState _sceneState;
        private IRetentionPhaseManager _retentionPhaseManager;
        
        [Inject]
        public void Init(ISceneState sceneState, IRetentionPhaseManager retentionPhaseManager) {
            _sceneState = sceneState;
            _retentionPhaseManager = retentionPhaseManager;
        }
        
        protected override void OnEnable() {
            base.OnEnable();
            Observe(_sceneState.OnUpdate);
            Observe(_retentionPhaseManager.OnPhaseCompleted);
            Observe(_retentionPhaseManager.OnPhaseStarted);
        }

        void IOnUpdate.Do(float timeDelta) {
            int currentTime = (int) _retentionPhaseManager.TimeLeft;
            if (_timeLeft == currentTime) {
                return;
            }
            _timeLeft = currentTime;
            _text.text = currentTime.ToString();
        }

        void IOnPhaseCompleted.Do() {
            _mainGameObject.SetActive(false);
        }

        void IOnPhaseStarted.Do() {
            _mainGameObject.SetActive(true);
        }
    }
}