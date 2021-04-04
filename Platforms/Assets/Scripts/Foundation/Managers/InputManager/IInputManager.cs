using Joysticks;

namespace Foundation
{
    public interface IInputManager
    {
        IInputSource InputForPlayer(int playerIndex);

        bool InputOverridenForPlayer(int playerIndex);
        void OverrideInputForPlayer(int playerIndex, IInputSource overrideSource);
        void RegisterJoystick(IJoystick joystick);
        void UnregisterJoystick();
        IJoystick Joystick { get; }
    }
}
