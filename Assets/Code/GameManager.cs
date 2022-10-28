using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu; //must link to settings, restart level, and quit
    public GameObject settingsMenu; // must have slider for music and sound fx volume & text for their int value
    public GameObject deathScreen; // must restart level 
    public PlayerCode player;
    public float musicVolume;
    public float soundEffectsVolume;
    public BunnyDialogueManager dialogueManager;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundEffectsSlider;
    [SerializeField] private TextMeshProUGUI musicSliderText;
    [SerializeField] private TextMeshProUGUI soundEffectsSliderText;
    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup soundEffectsMixerGroup;

    void Awake()
    {
        dialogueManager = BunnyDialogueManager.Instance;
    }

    void Start()
    {
        if(pauseMenu != null)
            pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        if(deathScreen != null)
            deathScreen.SetActive(false);
        if(player != null)
            player.paused = false;
        Time.timeScale = 1f;
        StartCoroutine(LoadOptions());
        GameObject playerMaybe = GameObject.FindGameObjectWithTag("Player");
        if (playerMaybe) {
            player = playerMaybe.GetComponent<PlayerCode>();
        } else {
            print("no player found in gamemanager");
        }
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu != null)
        if (Input.GetButtonDown("Cancel") && pauseMenu != null)
        {
            if (pauseMenu.activeInHierarchy)
            {
                ClosePauseMenu();
            } else
            {
                ShowPauseMenu();
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Introduction");
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
    
    public IEnumerator LoadOptions()
    {
        yield return new WaitForSeconds(0f);
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            SaveOptions();
        }
        musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        soundEffectsVolume = PlayerPrefs.GetFloat("SFXVolume");

        musicSlider.value = musicVolume;
        musicSliderText.text = ((int)(musicVolume * 100)).ToString();
        soundEffectsSlider.value = soundEffectsVolume;
        soundEffectsSliderText.text = ((int)(soundEffectsVolume * 100)).ToString();
    }

    public void SaveOptions()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", soundEffectsVolume);
        PlayerPrefs.Save();
    }

    public void UpdateMixerVolume()
    {
        musicMixerGroup.audioMixer.SetFloat("Music", Mathf.Log10(musicVolume) * 20);
        soundEffectsMixerGroup.audioMixer.SetFloat("SFX", Mathf.Log10(soundEffectsVolume) * 20);
    }

    public void ShowDeathScreen()
    {
        player.paused = true;
        deathScreen.SetActive(true);
        //Cursor.lockState = CursorLockMode.None;
    }

    public void ShowPauseMenu()
    {
        player.paused = true;
        pauseMenu.SetActive(true);
        //Cursor.lockState = CursorLockMode.None;
    }

    public void ShowSettingsMenu()
    {
        settingsMenu.SetActive(true);
    }

    public void CloseDeathScreen()
    {
        deathScreen.SetActive(false);
        player.paused = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }
    public void ClosePauseMenu()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        player.paused = false;
        //Cursor.lockState = CursorLockMode.Locked;
        SaveOptions();
    }
    public void CloseOptionsMenu()
    {
        SaveOptions();
        settingsMenu.SetActive(false);
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

     public void OnMusicSliderValueChange(float value)
    {
        musicVolume = value;

        musicSliderText.text = ((int)(value * 100)).ToString();
        UpdateMixerVolume();
        SaveOptions();
    }

    public void OnSoundEffectsSliderValueChange(float value)
    {
        soundEffectsVolume = value;

        soundEffectsSliderText.text = ((int)(value * 100)).ToString();
        UpdateMixerVolume();
        SaveOptions();
    }

    public void LoadNextLevel() 
    {
        string currlevel = SceneManager.GetActiveScene().name;
        int nextlevel = currlevel[currlevel.Length-1] - '0';
        if (nextlevel == 4) {
            SceneManager.LoadScene("WinScreen");
        } else {
            SceneManager.LoadScene("Level" + (++nextlevel).ToString());
        }
    }

}
