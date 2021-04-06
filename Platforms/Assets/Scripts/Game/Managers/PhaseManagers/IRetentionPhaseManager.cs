namespace Game.Managers.PhaseManagers
{
    public interface IRetentionPhaseManager: IPhaseManager
    {
        float TimeLeft { get; }
    }
}