namespace Foundation.Activities
{
    public abstract class CharacterActivity : SubscribableEntity
    {
        protected CharacterActivityView _view;
        private bool _isRunning;

        public CharacterActivityView View => _view;
        public bool IsRunning => _isRunning;
        public abstract ActivityType ActivityType { get; }

        public void SetUp(CharacterActivityView view)
        {
            _view = view;
        }
        
        public virtual void Start()
        {
            _isRunning = true;
            _view.OnStart();
        }

        public virtual void Finish()
        {
            _isRunning = false;
            _view.OnFinish();
            Reset();
        }

        public virtual void Cancel()
        {
            _isRunning = false;
            _view.OnCancel();
            Reset();
        }

        public virtual void Reset()
        {
            _view.OnReset();
        }

        public virtual bool CanBeReplacedBy(ActivityType activityType)
        {
            return true;
        }
    }
}