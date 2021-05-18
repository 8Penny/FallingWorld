using Foundation;
using Zenject;

namespace Game.Managers.InteractionManager {
    public class InteractionManager : AbstractService<IInteractionManager> {
        [Inject]
        public void Init() {
        }

        public void TryInteract() {
        }
    }
}