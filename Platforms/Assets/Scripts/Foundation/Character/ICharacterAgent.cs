using UnityEngine;

namespace Foundation
{
    public interface ICharacterAgent
    {
        float Speed { get; }
        Vector3 Position { get; }
        void Move(Vector2 dir);
        void SetPosition(Vector3 position);
        void SetSpeed(float value);
        void NavigateTo(Vector2 dir);
        void Look(Vector2 dir);
        void Stop();

        void SetConstantVelocity();
        void FreezeXZPositions();
        void UnfreezePositions();
    }
}
