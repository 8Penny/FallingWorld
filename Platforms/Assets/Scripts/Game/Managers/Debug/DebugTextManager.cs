using System;
using Foundation;
using Game.Managers.PlatformManager;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Managers.Debug {
    public class DebugTextManager : MonoBehaviour {
        [SerializeField]
        private TMP_Text _debugText;

        [SerializeField]
        private PlayerAgent _agent;

        [Inject] public IInputManager InputManager { get; set; }
        [Inject] public IPlatformManager PlatformManager { get; set; }
        
        
        private void Update() {
#if DEBUG
            _debugText.text = $"{_agent.Speed.ToString()} \n {_agent.CharacterTransform.position}";
            var input = InputManager.InputForPlayer(0);
            var wasFix = input.Action("DebugFix").Triggered;
            if (wasFix) {
                PlatformManager.DebugFixPlatform(0.5f);
            }
#endif
        }
    }
}