using System.Collections.Generic;
using Foundation;
using Game.Managers.PhaseManagers;
using Game.Managers.PlatformGeneratorManager;
using Zenject;

namespace Game.Managers
{
    public class MainSequenceManager : AbstractService<IMainSequenceManager>, IMainSequenceManager, IOnUpdate, IOnPhaseCompleted
    {
        private ISceneState _state;

        private GamePhase _currentPhase;
        private bool _isRunning;
        private Dictionary<GamePhase, IPhaseManager> _phaseManagers = new Dictionary<GamePhase, IPhaseManager>();

        private IRetentionPhaseManager _retention;
        private IActionPhaseManager _action;
        private IFallingPhaseManager _falling;

        private IPlatformGeneratorManager _platformGeneratorManager;

        [Inject]
        public void Init(IFallingPhaseManager fallingPhaseManager, IActionPhaseManager actionPhaseManager,
            IRetentionPhaseManager retentionPhaseManager, ISceneState state,
            IPlatformGeneratorManager platformGeneratorManager)
        {
            _state = state;
            
            _action = actionPhaseManager;
            _retention = retentionPhaseManager;
            _falling = fallingPhaseManager;
            
            _phaseManagers[GamePhase.Default] = retentionPhaseManager;
            _phaseManagers[GamePhase.Retention] = retentionPhaseManager;
            _phaseManagers[GamePhase.Action] = actionPhaseManager;
            _phaseManagers[GamePhase.Falling] = fallingPhaseManager;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Observe(_state.OnUpdate);
            
            Observe(_falling.OnPhaseCompleted);
            Observe(_retention.OnPhaseCompleted);
            Observe(_action.OnPhaseCompleted);
        }

        public void StartGame()
        {
            ResetGame();
            _platformGeneratorManager.GeneratePlatforms();

            _isRunning = true;
        }

        public void StopGame()
        {
            _isRunning = false;
        }

        private void ResetGame()
        {
            _currentPhase = GamePhase.Default;
            _retention.Reset();
            _action.Reset();
            _falling.Reset();
        }

        void IOnUpdate.Do(float timeDelta)
        {

        }

        void IOnPhaseCompleted.Do()
        {
            IPhaseManager phaseManager = _phaseManagers[_currentPhase];
            GamePhase nextPhase = phaseManager.NextPhase;
            IPhaseManager nextPhaseManager = _phaseManagers[nextPhase];

            _currentPhase = nextPhase;
            nextPhaseManager.Start();
        }
    }
}