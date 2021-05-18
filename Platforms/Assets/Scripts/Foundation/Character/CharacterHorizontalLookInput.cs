using UnityEngine;
using Zenject;

namespace Foundation
{
    public sealed class CharacterHorizontalLookInput : AbstractBehaviour, IOnUpdate
    {
        public string InputActionName;
        public Transform CharacterTransform;
        public float RotationSpeed;

        [Inject] IPlayer player = default;
        [Inject] IInputManager inputManager = default;
        [Inject] ISceneState sceneState = default;

        protected override void OnEnable()
        {
            base.OnEnable();
            Observe(sceneState.OnUpdate);
        }

        void IOnUpdate.Do(float timeDelta)
        {
            var input = inputManager.InputForPlayer(player.Index);
            var dir = input.Action(InputActionName).Vector2Value;

            if (!Mathf.Approximately(dir.x, 0.0f)) {
                CharacterTransform.localRotation *= Quaternion.AngleAxis(dir.x * RotationSpeed * timeDelta, Vector3.up);
            }
        }
    }
}
