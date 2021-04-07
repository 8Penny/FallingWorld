using Game.Managers.PhaseManagers;

namespace Game.Managers
{
    public interface IOnPhaseChanged
    {
        void Do(GamePhase newPhase);
    }
}