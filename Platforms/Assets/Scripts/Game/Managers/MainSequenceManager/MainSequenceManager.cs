using System.Collections.Generic;
using Foundation;
using Game.Managers.PhaseManagers;
using Zenject;

namespace Game.Managers
{
    public class MainSequenceManager : AbstractService<IMainSequenceManager>, IMainSequenceManager, IOnUpdate, IOnPhaseCompleted
    {
        private ICurrentGameStatsManager _gameStatsManager;
        private ISceneState _state;

        private IPhaseManager _currentPhaseManager;
        private bool _isRunning;
        private Dictionary<GamePhase, IPhaseManager> _phaseManagers = new Dictionary<GamePhase, IPhaseManager>();

        private IRetentionPhaseManager _retention;
        private IActionPhaseManager _action;
        private IFallingPhaseManager _falling;
        
        public IPhaseManager CurrentPhaseManager => _currentPhaseManager;
        public ObserverList<IOnPhaseCompleted> OnPhaseChanged { get; } = new ObserverList<IOnPhaseCompleted>();


        [Inject]
        public void Init(ICurrentGameStatsManager currentGameStatsManager,
            IFallingPhaseManager fallingPhaseManager, IActionPhaseManager actionPhaseManager,
            IRetentionPhaseManager retentionPhaseManager, ISceneState state)
        {
            _state = state;
            _gameStatsManager = currentGameStatsManager;
            
            _action = actionPhaseManager;
            _retention = retentionPhaseManager;
            _falling = fallingPhaseManager;
            
            _phaseManagers[GamePhase.Default] = fallingPhaseManager;
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
            
            StartGame();
        }

        public void StartGame()
        {
            ResetGame();

            _isRunning = true;
            StartNextPhase();
        }

        public void StopGame()
        {
            _isRunning = false;
        }

        private void ResetGame()
        {
            _gameStatsManager.SetGamePhase(GamePhase.Default);
            _retention.Reset();
            _action.Reset();
            _falling.Reset();
        }

        void IOnUpdate.Do(float timeDelta)
        {

        }

        void IOnPhaseCompleted.Do()
        {
            StartNextPhase();
        }

        private void StartNextPhase()
        {
            if (!_isRunning)
            {
                return;
            }

            if (_currentPhaseManager == null) {
                _currentPhaseManager = _phaseManagers[_gameStatsManager.CurrentGamePhase];
            }
            GamePhase nextPhase = _currentPhaseManager.NextPhase;
            ChangeState(nextPhase);
        }

        public void TryInteract() {
            _currentPhaseManager.OnInteract();
        }

        public void ForceChangeState(GamePhase state) {
            _currentPhaseManager.Reset();
            ChangeState(state);
        }

        private void ChangeState(GamePhase state) {
            IPhaseManager nextPhaseManager = _phaseManagers[state];

            _currentPhaseManager = nextPhaseManager;
            _gameStatsManager.SetGamePhase(state);
            _currentPhaseManager.StartPhase();
            DebugOnly.Message($"{state} start");
        }

    }
}