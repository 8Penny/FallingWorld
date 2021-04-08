using System;
using Foundation;
using Game.Components.UI;
using Game.Managers.PhaseManagers;
using Game.Managers.UIPoolManager;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class CurrentGameStatsManager : AbstractService<ICurrentGameStatsManager>, ICurrentGameStatsManager
    {
        // [SerializeField]
        // private ButtonView _view;
        public ObserverList<IOnPhaseChanged> OnPhaseChanged { get; } = new ObserverList<IOnPhaseChanged>();
        private GamePhase _currentGamePhase;
        public GamePhase CurrentGamePhase => _currentGamePhase;

        [Inject]
        public IUIPoolManager UIPoolManager;
        
        private void Awake()
        {
            //_view.SetPresenter(new ButtonPresenter(UIPoolManager));
        }
        public void SetGamePhase(GamePhase phase)
        {
            _currentGamePhase = phase;

            foreach (var it in OnPhaseChanged.Enumerate()) {
                it.Do(phase);
            }
        }
    }
}