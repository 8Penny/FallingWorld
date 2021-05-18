using Foundation;

namespace Game.Managers.Instantiator {
    public class Instantiator : AbstractService<IInstantiator>, IInstantiator {
        public T Instantiate<T>() {
            return Container.Instantiate<T>();
        }
    }
}