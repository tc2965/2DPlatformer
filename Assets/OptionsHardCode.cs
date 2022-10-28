using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OptionsHardCode : MonoBehaviour
{
    public GameObject ResumeButton;
    public Slider volumeBarMusic;
    public Slider volumeBarSfx;



    public void Restart(InputAction.CallbackContext context) {
        ClickButton(ResumeButton);
    }

    public void Music(InputAction.CallbackContext context) {
        if (context.performed) {
            Vector2 volume = context.ReadValue<Vector2>();
            volumeBarMusic.value += 0.05f * volume.x;
        }
    }

    public void Sfx(InputAction.CallbackContext context) {
        if (context.performed) {
            Vector2 volume = context.ReadValue<Vector2>();
            volumeBarSfx.value += 0.05f * volume.y;
        }
    }

    public void ClickButton(GameObject buttonObject) {
        Button button = buttonObject.GetComponent<Button>();
        button.onClick.Invoke();
    }
}
