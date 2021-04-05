using System;
using Foundation;

namespace Game.Managers.PhaseManagers
{
    public class FallingPhaseManager: AbstractService<IFallingPhaseManager>, IFallingPhaseManager
    {
        public ObserverList<IOnPhaseCompleted> OnPhaseCompleted { get; } = new ObserverList<IOnPhaseCompleted>();
        public GamePhase NextPhase => GamePhase.Retention;
        

        public void Finish()
        {
        }

        public void Reset()
        {
        }
    }
}