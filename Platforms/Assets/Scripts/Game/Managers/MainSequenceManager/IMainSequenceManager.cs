using Foundation;
using Game.Managers.PhaseManagers;

namespace Game.Managers
{
    public interface IMainSequenceManager {
        void TryInteract();
        void ForceChangeState(GamePhase state);
        IPhaseManager CurrentPhaseManager { get; }
        ObserverList<IOnPhaseCompleted> OnPhaseChanged { get; }
    }
}