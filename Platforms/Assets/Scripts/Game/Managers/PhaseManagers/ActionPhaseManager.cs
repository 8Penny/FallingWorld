using System;
using Foundation;

namespace Game.Managers.PhaseManagers
{
    public class ActionPhaseManager : AbstractService<IActionPhaseManager>, IActionPhaseManager
    {
        public ObserverList<IOnPhaseCompleted> OnPhaseCompleted { get; } = new ObserverList<IOnPhaseCompleted>();
        public GamePhase NextPhase => GamePhase.Falling;

        public void Finish()
        {
        }

        public void Reset()
        {
        }

        public event Action OnCompleted;
    }
}