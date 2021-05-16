using System.Collections.Generic;
using Foundation.Activities.Idle;
using Zenject;

namespace Foundation.Activities
{
    public class ActivityFabric
    {
        private Dictionary<ActivityType, Stack<CharacterActivity>> _pool =
            new Dictionary<ActivityType, Stack<CharacterActivity>>();

        private AnimatorController _animatorController;
        private IInstantiator _instantiator;

        public ActivityFabric(AnimatorController animator, IInstantiator instantiator) {
            _animatorController = animator;
            _instantiator = instantiator;
        }
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
                    activity = _instantiator.Instantiate<IdleActivity>();
                    view = new IdleActivityView();
                    break;
                case ActivityType.Movement:
                    activity = _instantiator.Instantiate<MovementActivity>();
                    view = new MovementActivityView();
                    break;
                default:
                    break;
            }
            
            view.SetUp(_animatorController);
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