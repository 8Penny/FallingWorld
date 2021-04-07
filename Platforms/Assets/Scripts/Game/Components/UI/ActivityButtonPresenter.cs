using Foundation;
using Game.Managers;
using Game.Managers.PhaseManagers;
using UnityEngine;
using Zenject;

namespace Game.Components.UI
{
    public class ActivityButtonPresenter : AbstractBehaviour, IOnPhaseChanged
    {
        private bool _isVisible;
        private bool _isInteractable;
        private ActivityButtonType _buttonType;

        private ICurrentGameStatsManager _gameStatsManager;

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
        }

        void IOnPhaseChanged.Do(GamePhase newPhase)
        {
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
        }
    }

    public enum ActivityButtonType
    {
        Fixation,
        Interaction
    }
}