namespace Foundation.Character.Input
{
    public interface ICharacterJumpInput
    {
        ObserverList<IOnCharacterJump> OnCharacterJump { get; }
    }
}
