using System.Collections.Generic;
using Foundation.Activities;
using Zenject;

namespace Foundation.Character.Activities.ActivityQueue
{
    public class CharacterActivityQueue : AbstractService<ICharacterActivityQueue>, ICharacterActivityQueue, IOnUpdate
    {
        private ISceneState _sceneState;
        private CharacterActivity _currentActivity;
        private ActivityFabric _activityFabric = new ActivityFabric();

        private Queue<ActivityType> _queue =
            new Queue<ActivityType>();

        public CharacterActivity CurrentActivity => _currentActivity;
        public CharacterActivityView CurrentActivityView => _currentActivity.View;

        [Inject]
        public void Init(ISceneState sceneState)
        {
            _sceneState = sceneState;
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();
            Observe(_sceneState.OnUpdate);
        }

        void IOnUpdate.Do(float timeDelta)
        {
            TryResetCurrentActivity();
            TryStartNewActivity();
            TryReplaceCurrentActivity();
        }

        private void TryStartNewActivity()
        {
            if (_currentActivity != null)
            {
                return;
            }

            if (_queue.Count != 0)
            {
                _currentActivity = _activityFabric.CreateActivity(_queue.Dequeue());
            }
            else
            {
                _currentActivity = _activityFabric.CreateActivity(ActivityType.Idle);
            }

            StartCurrentActivity();
        }

        private void TryResetCurrentActivity()
        {
            if (_currentActivity == null)
            {
                return;
            }

            if (!_currentActivity.IsRunning)
            {
                ResetCurrentActivity();
            }
        }

        private void TryReplaceCurrentActivity()
        {
            ActivityType possibleActivity = _currentActivity.ActivityType;
            
            while (true)
            {
                if (_queue.Count == 0)
                {
                    break;
                }
                var nextActivity = _queue.Peek();
                var canBeReplaced = _currentActivity.CanBeReplacedBy(nextActivity);
                if (canBeReplaced)
                {
                    possibleActivity = _queue.Dequeue();
                }
                else
                {
                    break;
                }
            }

            if (possibleActivity == _currentActivity.ActivityType)
            {
                return;
            }
            
            StopCurrentActivity();
            SetNewActivity(possibleActivity);
            StartCurrentActivity();
        }

        private void StartCurrentActivity()
        {
            if (_currentActivity == null)
            {
                DebugOnly.Error("Cant start current activity. Current activity is null.");
                return;
            }
            _currentActivity.Start();
            // notify
        }
        private void StopCurrentActivity()
        {
            if (_currentActivity == null)
            {
                DebugOnly.Error("Cant stop current activity. Current activity is null.");
                return;
            }
            _currentActivity.Cancel();
            // notify
        }

        private void SetNewActivity(ActivityType activityType)
        {
            ResetCurrentActivity();
            _currentActivity = _activityFabric.CreateActivity(activityType);
        }

        private void ResetCurrentActivity()
        {
            _activityFabric.ReturnActivity(_currentActivity);
            _currentActivity = null;
        }

        public void AddActivity(ActivityType activityType)
        {
            _queue.Enqueue(activityType);
        }

        public void Clear()
        {
            _currentActivity = null;
            _queue.Clear();
        }
    }
}