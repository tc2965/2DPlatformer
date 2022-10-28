using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DeathScreenHardCode : MonoBehaviour
{
    public GameObject RestartButton;
    public GameObject QuitButton;

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
