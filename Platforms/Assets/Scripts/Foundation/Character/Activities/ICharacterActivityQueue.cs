namespace Foundation.Activities
{
    public interface ICharacterActivityQueue
    {
        CharacterActivity CurrentActivity { get; }
        CharacterActivityView CurrentActivityView { get; }

        void AddActivity(ActivityType activityType);
        void Clear();
    }
}