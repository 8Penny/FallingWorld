using UnityEngine;
using Zenject;

namespace Foundation.Character.Input
{
    public sealed class CharacterInteractInput : AbstractBehaviour, IOnUpdate
    {
        public string InputActionName;

        ICharacterDialogs activeDialogs;

        [Inject] IPlayer player = default;
        [Inject] IInputManager inputManager = default;
        [Inject] ISceneState sceneState = default;
        [Inject] IDialogUI dialogUI = default;

        public void OnTriggerEnter(Collider other)
        {
            var context = other.GetComponentInParent<Context>();
            if (context != null) {
                var dialogs = context.Container.TryResolve<ICharacterDialogs>();
                if (dialogs != null && activeDialogs == null) {
                    activeDialogs = dialogs;
                }
            }
        }

        public void OnTriggerExit(Collider other)
        {
            var context = other.GetComponentInParent<Context>();
            if (context != null) {
                var dialogs = context.Container.TryResolve<ICharacterDialogs>();
                if (dialogs == activeDialogs) {
                    activeDialogs = null;
                }
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Observe(sceneState.OnUpdate);
        }

        void IOnUpdate.Do(float timeDelta)
        {
            if (activeDialogs != null) {
                var input = inputManager.InputForPlayer(player.Index);
                if (input.Action(InputActionName).Triggered) {
                    dialogUI.DisplayDialogs(player, activeDialogs.Portrait, activeDialogs.Dialogs);
                }
            }
        }
    }
}
