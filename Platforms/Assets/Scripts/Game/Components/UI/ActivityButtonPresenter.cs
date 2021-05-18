using Game.Components.Core;
using Game.Managers;
using Game.Managers.PhaseManagers;
using Zenject;

namespace Game.Components.UI
{
    public class ActivityButtonPresenter : ButtonPresenter, IOnPhaseChanged {
        private bool _isVisible = true;
        private bool _isInteractable = true;
        private ActivityButtonType _buttonType;

        private ICurrentGameStatsManager _gameStatsManager;
        private IMainSequenceManager _mainSequenceManager;

        public bool IsVisible => _isVisible;
        public bool IsInteractable => _isInteractable;
        public ActivityButtonType ButtonType => _buttonType;
        public FWEvent OnUpdated;

        [Inject]
        public void Init(ICurrentGameStatsManager gameStatsManager, IMainSequenceManager mainSequenceManager)
        {
            _gameStatsManager = gameStatsManager;
            _mainSequenceManager = mainSequenceManager;
            OnUpdated = CreateEvent();
            Observe(_gameStatsManager.OnPhaseChanged);
        }
        
        
        public override void OnViewAttached() {
            
            UpdateButtonType(_gameStatsManager.CurrentGamePhase);
        }

        public void Do(GamePhase newPhase) {
            UpdateButtonType(newPhase);
        }

        private void UpdateButtonType(GamePhase newPhase) {
            switch (newPhase)
            {
                case GamePhase.Action:
                    _buttonType = ActivityButtonType.Interaction;
                    break;
                case GamePhase.Retention:
                    _buttonType = ActivityButtonType.Fixation;
                    break;
                case GamePhase.Falling:
                    _isInteractable = false;
                    break;
            }
            OnUpdated.Invoke();
        }

        public override void OnButtonClick() {
            base.OnButtonClick();
            _mainSequenceManager.TryInteract();
        }
    }

    public enum ActivityButtonType
    {
        Fixation,
        Interaction
    }
}