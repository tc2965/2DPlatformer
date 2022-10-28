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
        }
        // } else if (!virtualMouse.added) {
        //     InputSystem.AddDevice("VirtualMouse");
        // }
        print(virtualMouse);

        InputUser.PerformPairingWithDevice(virtualMouse, playerInput.user);

        if (cursorTransform != null) {
            Vector2 position = cursorTransform.anchoredPosition;
            InputState.Change(virtualMouse.position, position);
        }
        // InputSystem.onBeforeUpdate += Testing;
        InputSystem.onAfterUpdate += UpdateMotion;
    }

    // private void Testing() 
    // {
    //     print("trying testing");
    // }

    private void OnDisable()
    {
        // if (virtualMouse != null && virtualMouse.added) {
        playerInput.user.UnpairDevice(virtualMouse);
        InputSystem.RemoveDevice(virtualMouse);
        // }
        InputSystem.onAfterUpdate -= UpdateMotion;
    }

    private void UpdateMotion() {
        Debug.Log(Gamepad.current);
        if (virtualMouse == null || Gamepad.current == null) {
            return;
        }

        Vector2 deltaValue = Gamepad.current.leftStick.ReadValue();
        deltaValue *= cursorSpeed * Time.deltaTime;
        print("deltaValue");
        print(deltaValue);

        Vector2 currentPosition = virtualMouse.position.ReadValue();
        Vector2 newPosition = currentPosition + deltaValue;

        newPosition.x = Mathf.Clamp(newPosition.x, 0, Screen.width);
        newPosition.y = Mathf.Clamp(newPosition.y, 0, Screen.height);

        InputState.Change(virtualMouse.position, newPosition);
        InputState.Change(virtualMouse.delta, deltaValue);
        print("virtualMouse.position");
        print(virtualMouse.position);

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
