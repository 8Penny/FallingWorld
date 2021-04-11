using System;
using Foundation;
using Zenject;

namespace Game.Managers.PhaseManagers
{
    public class FallingPhaseManager: AbstractService<IFallingPhaseManager>, IFallingPhaseManager, IOnUpdate
    {
        private float _timeLeft;
        private bool _isActive;
        private ISceneState _sceneState;
        
        public ObserverList<IOnPhaseCompleted> OnPhaseCompleted { get; } = new ObserverList<IOnPhaseCompleted>();
        public ObserverList<IOnPhaseStarted> OnPhaseStarted { get; } = new ObserverList<IOnPhaseStarted>();
        public GamePhase NextPhase => GamePhase.Retention;
        public bool IsActive => _isActive;

        private const float TIME = 15f; // TODO: to config

        [Inject]
        public void Init(ISceneState sceneState)
        {
            _sceneState = sceneState;
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();
            Observe(_sceneState.OnUpdate);
        }

        public void StartPhase()
        {
            _timeLeft = TIME;
            _isActive = true;
            
            foreach (var it in OnPhaseStarted.Enumerate()) {
                it.Do();
            }
        }

        public void Finish()
        {
            foreach (var it in OnPhaseCompleted.Enumerate()) {
                it.Do();
            }
            _isActive = false;
        }

        public void OnInteract() {
            
        }

        public void Reset()
        {
            _isActive = false;
            _timeLeft = 0;
        }

        void IOnUpdate.Do(float timeDelta)
        {
            if (!_isActive)
            {
                return;
            }
            
            if (_timeLeft > 0)
            {
                _timeLeft -= timeDelta;
                return;
            }
            Finish();
        }
    }
}