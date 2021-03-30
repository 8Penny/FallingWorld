using UnityEngine;
using Zenject;

namespace Foundation
{
    public sealed class CharacterAttackInput : AbstractBehaviour, IOnUpdate
    {
        public string InputActionName;

        [Inject] IPlayer player = default;
        [Inject] IInputManager inputManager = default;
        [Inject] ISceneState sceneState = default;
        [Inject] ICharacterWeapon weapon = default;

        protected override void OnEnable()
        {
            base.OnEnable();
            Observe(sceneState.OnUpdate);
        }

        void IOnUpdate.Do(float timeDelta)
        {
            var input = inputManager.InputForPlayer(player.Index);
            if (input.Action(InputActionName).Triggered)
                weapon.Attack();
        }
    }
}
