using System;
using Foundation;
using Joysticks;
using UnityEngine;
using Zenject;

public class FixedJoystick : Joystick, IJoystick {
    [Inject]
    public IInputManager InputManager;
    Vector2 IJoystick.Direction => Direction;

    private void OnEnable() {
        if (InputManager == null) {
            return;
        } 
        InputManager.RegisterJoystick(this);
    }

    private void OnDisable() {
        if (InputManager == null) {
            return;
        } 
        InputManager.UnregisterJoystick();
    }
}