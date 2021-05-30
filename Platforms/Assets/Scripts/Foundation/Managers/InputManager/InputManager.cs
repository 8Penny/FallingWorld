using System.Collections.Generic;
using Joysticks;
using Zenject;

namespace Foundation {
    public sealed class InputManager : AbstractService<IInputManager>, IInputManager {
        [Inject]
        public InputSource.Factory inputSourceFactory = default;

        private List<IInputSource> _inputSources = new List<IInputSource>();
        private List<IInputSource> _inputSourceOverrides = new List<IInputSource>();

        private bool _isInputAvailable = true;
        private IJoystick _joystick;
        
        public IJoystick Joystick {
            get {
                if (!_isInputAvailable) {
                    return default;
                }
                return _joystick;
            }
        }

        public bool IsInputAvailable => _isInputAvailable;

        private void Awake() {
            _inputSources.Add(inputSourceFactory.Create());
        }

        public IInputSource InputForPlayer(int playerIndex) {
            if (playerIndex >= 0) {
                if (playerIndex < _inputSourceOverrides.Count && _inputSourceOverrides[playerIndex] != null) {
                    return _inputSourceOverrides[playerIndex];
                }

                if (playerIndex < _inputSources.Count && _inputSources[playerIndex] != null) {
                    return _inputSources[playerIndex];
                }
            }

            return DummyInputSource.Instance;
        }

        public bool InputOverridenForPlayer(int playerIndex) {
            return (playerIndex >= 0
                    && playerIndex < _inputSourceOverrides.Count
                    && _inputSourceOverrides[playerIndex] != null);
        }

        public void OverrideInputForPlayer(int playerIndex, IInputSource overrideSource) {
            DebugOnly.Check(playerIndex >= 0, "Invalid player index.");

            if (overrideSource == null) {
                if (playerIndex < _inputSourceOverrides.Count) {
                    _inputSourceOverrides[playerIndex] = null;
                }

                return;
            }

            DebugOnly.Check(!InputOverridenForPlayer(playerIndex),
                "Attempted to install multiple overrides for player input.");

            while (playerIndex >= _inputSourceOverrides.Count) {
                _inputSourceOverrides.Add(null);
            }

            _inputSourceOverrides[playerIndex] = overrideSource;
        }

        public void RegisterJoystick(IJoystick joystick) {
            _joystick = joystick;
        }
        
        public void UnregisterJoystick() {
            _joystick = null;
        }

        public void SetInputAvailable(bool isAvailable) {
            _isInputAvailable = isAvailable;
        }
    }
}