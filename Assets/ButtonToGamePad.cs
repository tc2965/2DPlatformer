using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonToGamePad : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject OptionsButton; 
    public GameObject QuitButton;

    public void Play(InputAction.CallbackContext context) {
        Button play = PlayButton.GetComponent<Button>();
        play.onClick.Invoke();
    }

    public void Options(InputAction.CallbackContext context) {
        Button options = OptionsButton.GetComponent<Button>();
        options.onClick.Invoke();
    }

    public void Quit(InputAction.CallbackContext context) {
        Button quit = QuitButton.GetComponent<Button>();
        quit.onClick.Invoke();
    }
}
