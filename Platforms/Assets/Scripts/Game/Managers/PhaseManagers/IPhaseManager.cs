using System;
using Foundation;

namespace Game.Managers.PhaseManagers
{
    public interface IPhaseManager
    {
        ObserverList<IOnPhaseCompleted> OnPhaseCompleted { get; }
        GamePhase NextPhase { get; }
        void Start();
        void Finish();

        void Reset();
    }
}