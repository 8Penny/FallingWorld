using Game.Managers.PhaseManagers;

namespace Game.Managers
{
    public interface ICurrentGameStatsManager
    {
        GamePhase CurrentGamePhase { get; }
        void SetGamePhase(GamePhase phase);
    }
}