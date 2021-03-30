namespace Foundation
{
    public sealed class CharacterAnimationEvents : AbstractService<CharacterAnimationEvents>
    {
        public readonly ObserverList<IOnAttackAnimationEnded> OnAttackEnded = new ObserverList<IOnAttackAnimationEnded>();

        void AttackEnded()
        {
            foreach (var it in OnAttackEnded.Enumerate())
                it.Do();
        }
    }
}
