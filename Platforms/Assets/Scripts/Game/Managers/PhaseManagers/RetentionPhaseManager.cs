using System;
using Foundation;
using Game.Managers.PlatformManager;
using Zenject;

namespace Game.Managers.PhaseManagers
{
    public class RetentionPhaseManager : AbstractService<IRetentionPhaseManager>, IRetentionPhaseManager, IOnUpdate
    {
        private float _timeLeft;
        private bool _isActive;
        private ISceneState _sceneState;
        private IPlatformManager _platformManager;
        
        public ObserverList<IOnPhaseCompleted> OnPhaseCompleted { get; } = new ObserverList<IOnPhaseCompleted>();
        public ObserverList<IOnPhaseStarted> OnPhaseStarted { get; }  = new ObserverList<IOnPhaseStarted>();
        public GamePhase NextPhase => GamePhase.Action;
        public bool IsActive => _isActive;
        public float TimeLeft => _timeLeft;

        private const float TIME = 3f; // TODO: to config

        [Inject]
        public void Init(ISceneState sceneState, IPlatformManager platformManager)
        {
            _sceneState = sceneState;
            _platformManager = platformManager;
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
            var fallingPlatforms = _platformManager.GetFallingPlatforms();
            for (int i = 0; i < fallingPlatforms.Count; i++)
            {
                fallingPlatforms[i].SetFallingSpeed(UnityEngine.Random.Range(0.4f, 1.1f));
                fallingPlatforms[i].StartFalling();
            }
            
            foreach (var it in OnPhaseCompleted.Enumerate()) {
                it.Do();
            }
            _isActive = false;
            DebugOnly.Message("FinishedRetention");
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