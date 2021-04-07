using Foundation;
using Game.Managers.PhaseManagers;

namespace Game.Managers
{
    public class CurrentGameStatsManager : AbstractService<ICurrentGameStatsManager>, ICurrentGameStatsManager
    {
        public ObserverList<IOnPhaseChanged> OnPhaseChanged { get; } = new ObserverList<IOnPhaseChanged>();
        private GamePhase _currentGamePhase;
        public GamePhase CurrentGamePhase => _currentGamePhase;

        public void SetGamePhase(GamePhase phase)
        {
            _currentGamePhase = phase;

            foreach (var it in OnPhaseChanged.Enumerate()) {
                it.Do(phase);
            }
        }
    }
}