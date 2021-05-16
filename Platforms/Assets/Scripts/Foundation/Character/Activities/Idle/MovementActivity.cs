using System;
using Zenject;

namespace Foundation.Activities.Idle {
    public class MovementActivity : CharacterActivity, IOnUpdate {
        public override ActivityType ActivityType => ActivityType.Movement;

        private IPlayer _player;
        private ISceneState _sceneState;

        private MovementActivityView _movementActivityView;

        [Inject]
        public void Init(IPlayer player, ISceneState sceneState) {
            _player = player;
            _sceneState = sceneState;
        }

        public override void Start() {
            base.Start();
            _movementActivityView = _view as MovementActivityView;
            Observe(_sceneState.OnUpdate);
        }

        public void Do(float timeDelta) {
            _movementActivityView.UpdateSpeed(_player.Agent.Speed);

            if (_player.Agent.Speed < 0.01f) {
                Finish();
            }
        }

        public override void Reset() {
            base.Reset();
            Clear();
        }
    }
}