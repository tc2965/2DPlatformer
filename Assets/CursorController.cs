using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

public class CursorController : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private RectTransform cursorTransform;
    [SerializeField]
    private float cursorSpeed = 1000f;
    [SerializeField]
    private RectTransform canvasRectTransform;
    [SerializeField]
    private Canvas canvas;

    private bool previousMouseState;
    private bool togglePause = false;
    private Mouse virtualMouse;
    private Camera mainCamera;
    private GamepadCursor controls;
    private GamepadCursor.PlayerActions playerActions;
    private GamepadCursor.UIActions uIActions;

    // private void Awake()
    // {
    //     controls = new GamepadCursor();
    //     playerActions = controls.Player;
    //     uIActions = controls.UI;


    //     playerActions.Disable();
    //     uIActions.Enable();

    //     uIActions.VirtualMouseValue.performed += _ => UpdateMotion();
    // }

    private void OnEnable() {
        mainCamera = Camera.main;
        if (virtualMouse == null) {
            virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");
        } else if (!virtualMouse.added) {
            InputSystem.AddDevice("VirtualMouse");
        }

        InputUser.PerformPairingWithDevice(virtualMouse, playerInput.user);

        if (cursorTransform != null) {
            Vector2 position = cursorTransform.anchoredPosition;
            InputState.Change(virtualMouse.position, position);
        }
        InputSystem.onAfterUpdate += UpdateMotion;
    }

    private void OnDisable()
    {
        InputSystem.RemoveDevice(virtualMouse);
        InputSystem.onAfterUpdate -= UpdateMotion;
        // playerInput.onControlsChanged -= onControlsChanged;
    }

    private void UpdateMotion() {
        if (virtualMouse == null || Gamepad.current == null) {
            return;
        }

        Vector2 deltaValue = Gamepad.current.leftStick.ReadValue();
        deltaValue *= cursorSpeed * Time.deltaTime;

        Vector2 currentPosition = virtualMouse.position.ReadValue();
        Vector2 newPosition = currentPosition + deltaValue;

        newPosition.x = Mathf.Clamp(newPosition.x, 0, Screen.width);
        newPosition.y = Mathf.Clamp(newPosition.y, 0, Screen.height);

        InputState.Change(virtualMouse.position, newPosition);
        InputState.Change(virtualMouse.delta, deltaValue);

        bool aButtonIsPressed = Gamepad.current.aButton.IsPressed();
        if (previousMouseState != aButtonIsPressed) {
            virtualMouse.CopyState<MouseState>(out var mouseState);
            mouseState.WithButton(MouseButton.Left, aButtonIsPressed);
            InputState.Change(virtualMouse, mouseState);
            previousMouseState = aButtonIsPressed;
        }

        AnchorCursor(newPosition);

    }

    private void AnchorCursor(Vector2 position) {
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform, position, 
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : mainCamera, 
            out anchoredPosition);
        cursorTransform.anchoredPosition = anchoredPosition;
    }

}
