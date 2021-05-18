namespace Game.Managers.Instantiator {
    public interface IInstantiator {
        T Instantiate<T>();
    }
}