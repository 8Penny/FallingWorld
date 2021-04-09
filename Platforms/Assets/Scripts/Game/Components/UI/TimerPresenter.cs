using Foundation;
using Game.Managers.PhaseManagers;
using Game.Managers.UIPoolManager;
using Zenject;

namespace Game.Components.UI
{
    public class TimerPresenter : UIPresenter, IOnUpdate, IOnPhaseCompleted, IOnPhaseStarted
    {
        private int _timeLeft;
        
        private ISceneState _sceneState;
        private IRetentionPhaseManager _retentionPhaseManager;
        private IUIPoolManager _uiPoolManager;

        public int TimeLeft => _timeLeft;
        public bool IsVisible => _retentionPhaseManager.IsActive;

        public UIEvent OnTimerUpdated;
        public UIEvent OnRetentionEnded;
        public UIEvent OnRetentionStarted;
        
        [Inject]
        public void Init(ISceneState sceneState, IRetentionPhaseManager retentionPhaseManager) {
            _sceneState = sceneState;
            _retentionPhaseManager = retentionPhaseManager;

            Observe(_sceneState.OnUpdate);
            Observe(_retentionPhaseManager.OnPhaseCompleted);
            Observe(_retentionPhaseManager.OnPhaseStarted);

            OnRetentionEnded = CreateEvent();
            OnRetentionStarted = CreateEvent();
            OnTimerUpdated = CreateEvent();
        }
        
        void IOnUpdate.Do(float timeDelta) {
            int currentTime = (int) _retentionPhaseManager.TimeLeft;
            if (_timeLeft == currentTime) {
                return;
            }
            _timeLeft = currentTime;
            OnTimerUpdated.Invoke();
        }

        void IOnPhaseCompleted.Do() {
            OnRetentionEnded.Invoke();
        }

        void IOnPhaseStarted.Do() {
            DebugOnly.Message("RETENTION STARTED");
            OnRetentionStarted.Invoke();
        }
    }
}