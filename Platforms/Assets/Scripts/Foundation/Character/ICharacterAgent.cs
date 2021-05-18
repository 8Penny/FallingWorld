using UnityEngine;

namespace Foundation
{
    public interface ICharacterAgent
    {
        float Speed { get; }
        void Move(Vector2 dir);
        void SetSpeed(float value);
        void NavigateTo(Vector2 dir);
        void Look(Vector2 dir);
        void Stop();
    }
}
