using System;
using Foundation;

namespace Game.Managers.PhaseManagers
{
    public class RetentionPhaseManager : AbstractService<IRetentionPhaseManager>, IRetentionPhaseManager
    {
        public ObserverList<IOnPhaseCompleted> OnPhaseCompleted { get; } = new ObserverList<IOnPhaseCompleted>();
        public GamePhase NextPhase => GamePhase.Action;
        public void Finish()
        {
        }

        public void Reset()
        {
        }

        public event Action OnCompleted;
    }
}