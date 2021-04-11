using System.Collections.Generic;
using Foundation;
using Game.Managers.PhaseManagers;
using Game.Managers.PlatformGeneratorManager;
using Zenject;

namespace Game.Managers.PlatformManager
{
    public class PlatformManager : AbstractService<IPlatformManager>, IPlatformManager, IOnPlayerAdded
    {
        private ICurrentGameStatsManager _currentGameStatsManager;
        private IPlatformGeneratorManager _platformGenerator;
        private IPlayerManager _playerManager;
        private IPlayer _player;
        
        private List<Platform> _platforms = new List<Platform>();
        private List<Platform> _fallingPlatforms = new List<Platform>();
        private Platform[] _availablePlatforms = new Platform[4];
        private Platform _selectablePlatform;
        
        [Inject]
        public void Init(ICurrentGameStatsManager currentGameStatsManager,
            IPlatformGeneratorManager platformGeneratorManager,
            IPlayerManager playerManager)
        {
            _platformGenerator = platformGeneratorManager;
            _playerManager = playerManager;
            _currentGameStatsManager = currentGameStatsManager;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            GeneratePlatforms();
            Observe(_playerManager.OnPlayerAdded);
        }

        public void GeneratePlatforms()
        {
            _platforms = _platformGenerator.GeneratePlatforms();
        }
        
        public void ResetAvailablePlatforms()
        {
            for (int i = 0; i < _availablePlatforms.Length; i++)
            {
                _availablePlatforms[i] = null;
            }
        }

        public List<Platform> GetFallingPlatforms()
        {
            _fallingPlatforms.Clear();
            for (int i = 0; i < _platforms.Count; i++)
            {
                if (_platforms[i].Status == PlatformStatus.Fixed)
                {
                    continue;
                }
                _fallingPlatforms.Add(_platforms[i]);
            }
            return _fallingPlatforms;
        }

        public void BecomeAvailable(Platform platform)
        {
            if (_currentGameStatsManager.CurrentGamePhase != GamePhase.Retention)
            {
                return;
            }
            if (platform.Status == PlatformStatus.Fixed)
            {
                return;
            }
            for (int i = 0; i < _availablePlatforms.Length; i++)
            {
                if (_availablePlatforms[i] == null)
                {
                    _availablePlatforms[i] = platform;
                    break;
                }
            }

            UpdateSelectedPlatform();
        }

        public void BecomeNotAvailable(Platform platform)
        {
            if (_currentGameStatsManager.CurrentGamePhase  != GamePhase.Retention)
            {
                return;
            }
            for (int i = 0; i < _availablePlatforms.Length; i++)
            {
                if (_availablePlatforms[i] == platform)
                {
                    _availablePlatforms[i] = null;
                    break;
                }
            }

            UpdateSelectedPlatform();
        }

        public void TryFixPlatform() {
            _selectablePlatform.SetStatus(PlatformStatus.Fixed);
            UpdateSelectedPlatform();
        }

        private void UpdateSelectedPlatform()
        {
            int selectedIndex = -1;
            float minDistance = float.PositiveInfinity;
            for (int i = 0; i < _availablePlatforms.Length; i++)
            {
                var currentPlatform = _availablePlatforms[i];
                if (currentPlatform == null || currentPlatform.Status == PlatformStatus.Fixed)
                {
                    continue;
                }

                var distance = (_player.Position - currentPlatform.Position).sqrMagnitude;
                if (distance < minDistance)
                {
                    selectedIndex = i;
                    minDistance = distance;
                }
            }
            

            if (selectedIndex == -1)
            {
                if (_selectablePlatform != null)
                {
                    if (_selectablePlatform.Status != PlatformStatus.Fixed) {
                        _selectablePlatform.SetStatus(PlatformStatus.Default);
                    }
                }
                _selectablePlatform = null;
                return;
            }

            var nextSelectedPlatform = _availablePlatforms[selectedIndex];

            if (nextSelectedPlatform == _selectablePlatform)
            {
                return;
            }
            if (_selectablePlatform != null)
            {
                if (_selectablePlatform.Status != PlatformStatus.Fixed) {
                    _selectablePlatform.SetStatus(PlatformStatus.Default);
                }
            }
            nextSelectedPlatform.SetStatus(PlatformStatus.Selectable);

            _selectablePlatform = nextSelectedPlatform;
        }

        void IOnPlayerAdded.Do(int playerIndex)
        {
            _player = _playerManager.GetPlayer(playerIndex);
        }
    }
}