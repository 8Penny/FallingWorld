using System.Collections;
using System.Collections.Generic;
using Foundation.Activities.Idle;

namespace Foundation.Activities
{
    public class ActivityFabric
    {
        private Dictionary<ActivityType, Stack<CharacterActivity>> _pool =
            new Dictionary<ActivityType, Stack<CharacterActivity>>();
        public CharacterActivity CreateActivity(ActivityType activityType)
        {

            if (_pool.TryGetValue(activityType, out var activities))
            {
                if (activities.Count != 0)
                {
                    return activities.Pop();
                }
            }

            CharacterActivity activity = default;
            CharacterActivityView view = default;
            switch (activityType)
            {
                case ActivityType.Idle:
                    activity = new IdleActivity();
                    view = new IdleActivityView();
                    break;
                default:
                    break;
            }
            
            activity?.SetUp(view);
            return activity;
        }

        public void ReturnActivity(CharacterActivity activity)
        {
            if (activity == null)
            {
                DebugOnly.Error("Returning activity is null");
                return;
            }
            
            activity.Reset();
            if (_pool.TryGetValue(activity.ActivityType, out var activities))
            {
                activities.Push(activity);
                return;
            }

            var newStack = new Stack<CharacterActivity>();
            newStack.Push(activity);
            _pool[activity.ActivityType] = newStack;
        }
    }
}