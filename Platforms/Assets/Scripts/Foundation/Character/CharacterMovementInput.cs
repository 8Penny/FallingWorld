using System;
using Joysticks;
using UnityEngine;
using Zenject;

namespace Foundation
{
    public sealed class CharacterMovementInput : AbstractBehaviour, IOnUpdate
    {
        public string InputActionName;
        public float ForwardMovementSpeed;
        public float SideMovementSpeed;

        [Inject] 
        public IPlayer player = default;
        [Inject]
        public IInputManager inputManager = default;
        [Inject]
        public ICharacterAgent agent = default;
        [Inject]
        public ISceneState sceneState = default;

        protected override void OnEnable()
        {
            base.OnEnable();
            Observe(sceneState.OnUpdate);
        }

        void IOnUpdate.Do(float timeDelta)
        {
            var input = inputManager.InputForPlayer(player.Index);
            
            var dir = inputManager.Joystick.Direction;
            if (Mathf.Approximately(dir.sqrMagnitude, 0.0f)) {
#if UNITY_EDITOR
                dir = input.Action(InputActionName).Vector2Value;
                if (Mathf.Approximately(dir.sqrMagnitude, 0.0f)) {
                    agent.Stop();
                }
                else {
                    MovePlayer(dir, timeDelta);
                }      
#endif
            }
            else {
                MovePlayer(dir, timeDelta);
            }
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
        }
    }
}
