using System;
using Foundation;
using Foundation.Activities;
using Game.Managers.PlatformManager;
using UnityEngine;
using Zenject;

namespace Game.Managers.PhaseManagers
{
    public class FallingPhaseManager: AbstractService<IFallingPhaseManager>, IFallingPhaseManager, IOnUpdate, IOnLateUpdate
    {
        private const float TIME = 7f; // TODO: to config
        private const float TELEPORT_TIME = 2f; // TODO: to config
        
        private float _timeLeft;
        private bool _isActive;
        private ISceneState _sceneState;
        private IPlayerManager _playerManager;
        private IInputManager _inputManager;
        private IPlatformManager _platformManager;

        private IPlayer _player;
        private bool _wasTeleported;
        
        public ObserverList<IOnPhaseCompleted> OnPhaseCompleted { get; } = new ObserverList<IOnPhaseCompleted>();
        public ObserverList<IOnPhaseStarted> OnPhaseStarted { get; } = new ObserverList<IOnPhaseStarted>();
        public GamePhase NextPhase => GamePhase.Retention;
        public bool IsActive => _isActive;
        
        [Inject]
        public void Init(ISceneState sceneState, IPlayerManager playerManager, IInputManager inputManager, IPlatformManager platformManager)
        {
            _sceneState = sceneState;
            _playerManager = playerManager;
            _inputManager = inputManager;
            _platformManager = platformManager;
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();
            Observe(_sceneState.OnUpdate);
            Observe(_sceneState.OnLateUpdate);
        }

        public void StartPhase()
        {
            _timeLeft = TIME;
            _isActive = true;
            _wasTeleported = false;

            _player = _playerManager.GetPlayer(PlayerStatic.Id);
            _player.Agent.FreezeXZPositions();
            
            _player.ActivityQueue.AddActivity(ActivityType.Falling);
            
            _inputManager.SetInputAvailable(false);

            foreach (var it in OnPhaseStarted.Enumerate()) {
                it.Do();
            }
        }

        public void Finish()
        {
            _player?.ActivityQueue.AddActivity(ActivityType.Movement);
            _inputManager.SetInputAvailable(true);
            _player?.Agent.UnfreezePositions();

            foreach (var it in OnPhaseCompleted.Enumerate()) {
                it.Do();
            }
            _isActive = false;
        }

        public void OnInteract() {
            
        }

        public void Reset() {
            Finish();
            _timeLeft = 0;
        }

        void IOnUpdate.Do(float timeDelta)
        {
            if (!_isActive)
            {
                return;
            }
            
            if (_timeLeft < 0)
            {
                //Finish();
                return;
            }

            if (!_wasTeleported && _timeLeft < TELEPORT_TIME) {
                _wasTeleported = true;
                _player.Agent.SetPosition(new Vector3(0, 4, 0));
                _platformManager.GeneratePlatforms();
            }
            _timeLeft -= timeDelta;
        }

        void IOnLateUpdate.Do(float timeDelta) {
            if (!_isActive)
            {
                return;
            }
            _player.Agent.SetConstantVelocity();
        }
    }
}