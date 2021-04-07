using Foundation;
using Game.Managers;
using Game.Managers.PhaseManagers;
using UnityEngine;
using Zenject;

namespace Game.Components.UI
{
    public class ActivityButtonPresenter : AbstractBehaviour, IOnPhaseChanged {
        [SerializeField]
        private ActivityButtonView _view;
        
        private bool _isVisible;
        private bool _isInteractable;
        private ActivityButtonType _buttonType;

        private ICurrentGameStatsManager _gameStatsManager;

        public bool IsVisible => _isVisible;
        public bool IsInteractable => _isInteractable;
        public ActivityButtonType ButtonType => _buttonType;
        
        [Inject]
        public void Init(ICurrentGameStatsManager gameStatsManager)
        {
            _gameStatsManager = gameStatsManager;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Observe(_gameStatsManager.OnPhaseChanged);
            _isVisible = true;
            UpdateButtonType(_gameStatsManager.CurrentGamePhase);
            _view.UpdateParameters();
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
            
            _view.UpdateParameters();
        }

        public void OnClick() {
            
        }
    }

    public enum ActivityButtonType
    {
        Fixation,
        Interaction
    }
}