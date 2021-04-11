using System;
using Foundation;

namespace Game.Managers.PhaseManagers
{
    public interface IPhaseManager
    {
        ObserverList<IOnPhaseCompleted> OnPhaseCompleted { get; }
        ObserverList<IOnPhaseStarted> OnPhaseStarted { get; }
        GamePhase NextPhase { get; }
        bool IsActive { get; }
        void StartPhase();
        void Finish();
        void OnInteract();

        void Reset();
    }
}