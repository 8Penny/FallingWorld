using Foundation;
using Game.Managers.PhaseManagers;

namespace Game.Managers
{
    public class CurrentGameStatsManager : AbstractService<ICurrentGameStatsManager>, ICurrentGameStatsManager
    {
        private GamePhase _currentGamePhase;
        public GamePhase CurrentGamePhase => _currentGamePhase;

        public void SetGamePhase(GamePhase phase)
        {
            _currentGamePhase = phase;
        }
    }
}