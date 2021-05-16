using System;
using Foundation;
using Game.Components.UI;
using Game.Managers.PhaseManagers;
using Game.Managers.UIPoolManager;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class CurrentGameStatsManager : AbstractService<ICurrentGameStatsManager>, ICurrentGameStatsManager
    {
        [SerializeField]
        private TMP_Text _debugText;
        
        private GamePhase _currentGamePhase;
        public ObserverList<IOnPhaseChanged> OnPhaseChanged { get; } = new ObserverList<IOnPhaseChanged>();
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

        public void SetDebugText(string text) {
            _debugText.text = text;
        }
    }
}