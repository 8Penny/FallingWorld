
using Foundation.Activities;
using UnityEngine;
using Zenject;

namespace Foundation.Character.Input
{
    public sealed class CharacterMovementInput : AbstractBehaviour, IOnFixedUpdate
    {
        public string InputActionName;
        public float ForwardMovementSpeed; //TODO: to config
        public float SideMovementSpeed; //TODO: to config

        [Inject] 
        public IPlayer player = default;
        [Inject]
        public IInputManager inputManager = default;
        [Inject]
        public ICharacterAgent agent = default;
        [Inject]
        public ICharacterActivityQueue activityQueue = default;
        [Inject]
        public ISceneState sceneState = default;

        protected override void OnEnable()
        {
            base.OnEnable();
            Observe(sceneState.OnFixedUpdate);
        }

        void IOnFixedUpdate.Do(float timeDelta)
        {
            var input = inputManager.InputForPlayer(player.Index);
            
            var dir = inputManager.Joystick?.Direction ?? Vector3.zero;
            if (TryMove(dir, timeDelta)) {
                return;
            }
#if UNITY_EDITOR
            TryMove(input.Action(InputActionName).Vector2Value, timeDelta);
#endif
            }

        private bool TryMove(Vector2 dir, float timeDelta) {
            bool isZeroDirection = Mathf.Approximately(dir.sqrMagnitude, 0.0f);
            if (isZeroDirection) {
                agent.Stop();
                return false;
            }
            MovePlayer(dir, timeDelta);
            return true;
        }
        
        private void MovePlayer(Vector2 dir, float timeDelta) {
            Vector3 right3d = transform.right;
            Vector2 right2d = new Vector2(right3d.x, right3d.z);
            right2d.Normalize();
            right2d *= dir.x * SideMovementSpeed * timeDelta;

            Vector3 forward3d = transform.forward;
            Vector2 forward2d = new Vector2(forward3d.x, forward3d.z);
            forward2d.Normalize();
            forward2d *= dir.y * ForwardMovementSpeed * timeDelta;

            agent.Move(forward2d + right2d);
            agent.SetSpeed(Mathf.Abs(dir.magnitude));
            
            if (activityQueue.CurrentActivity?.ActivityType != ActivityType.Movement) {
                activityQueue.AddActivity(ActivityType.Movement);
            }
        }
    }
}
