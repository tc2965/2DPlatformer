using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WinScreenGamePadHardCode : MonoBehaviour
{
    public GameObject MainMenuButton;
    public GameObject OptionsButton; 
    public GameObject QuitButton;
    // public GameManager gameManager;

    public void MainMenu() {
        ClickButton(MainMenuButton);
    }

    public void Options() {
        ClickButton(OptionsButton);
    }

    public void Quit() {
        ClickButton(QuitButton);
    }

    public void ClickButton(GameObject buttonObject) {
        Button button = buttonObject.GetComponent<Button>();
        button.onClick.Invoke();
    }
}
