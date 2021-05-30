using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Foundation;
using Game.Managers;
using Game.Managers.PhaseManagers;
using UnityEngine;
using Zenject;
using IOnPhaseChanged = Game.Managers.IOnPhaseChanged;

public class TestColliderTrigger : AbstractBehaviour, IOnPhaseChanged {
    [SerializeField]
    private GamePhase _changePhase;
    [SerializeField]
    private List<GamePhase> _activePhases;
    [SerializeField]
    private Collider _collider;

    [Inject]
    public ICurrentGameStatsManager CurrentGameStatsManager { set; get; }
    [Inject]
    public IMainSequenceManager MainSequenceManager { set; get; }


    private bool _isActive = false;

    protected override void OnEnable() {
        Observe(CurrentGameStatsManager.OnPhaseChanged);
        _isActive = _activePhases.Contains(CurrentGameStatsManager.CurrentGamePhase);
    }

    private void OnTriggerEnter(Collider other) {
        if (!_isActive) {
            return;
        }
        MainSequenceManager.ForceChangeState(_changePhase);
    }

    void IOnPhaseChanged.Do(GamePhase phase) {
        _isActive = _activePhases.Contains(CurrentGameStatsManager.CurrentGamePhase);
        _collider.enabled = _isActive;
    }
}
