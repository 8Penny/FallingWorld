using System;
using Foundation;

namespace Game.Managers.PhaseManagers
{
    public class ActionPhaseManager : AbstractService<IActionPhaseManager>, IActionPhaseManager
    {
        private bool _isActive;
        public ObserverList<IOnPhaseCompleted> OnPhaseCompleted { get; } = new ObserverList<IOnPhaseCompleted>();
        public ObserverList<IOnPhaseStarted> OnPhaseStarted { get; } = new ObserverList<IOnPhaseStarted>();
        public GamePhase NextPhase => GamePhase.Falling;
        public bool IsActive => _isActive;

        public void StartPhase()
        {
            _isActive = true;
            foreach (var it in OnPhaseStarted.Enumerate())
            {
                it.Do();
            }
        }

        public void Finish()
        {
            foreach (var it in OnPhaseCompleted.Enumerate()) {
                it.Do();
            }
            _isActive = false;
        }

        public void Reset()
        {
            _isActive = false;
        }
    }
}