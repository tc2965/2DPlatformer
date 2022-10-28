using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenuHardCode : MonoBehaviour
{
    public GameObject ResumeButton;
    public GameObject RestartButton;
    public GameObject OptionsButton; 
    public GameObject QuitButton;

    public void Play(InputAction.CallbackContext context) {
        ClickButton(ResumeButton);
    }

    public void Options(InputAction.CallbackContext context) {
        ClickButton(OptionsButton);
    }

    public void Quit(InputAction.CallbackContext context) {
        ClickButton(QuitButton);
    }

    public void Restart(InputAction.CallbackContext context) {
        ClickButton(RestartButton);
    }

    public void ClickButton(GameObject buttonObject) {
        Button button = buttonObject.GetComponent<Button>();
        button.onClick.Invoke();
    }
}
