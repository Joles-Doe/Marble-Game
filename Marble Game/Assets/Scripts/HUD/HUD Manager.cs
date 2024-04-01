using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    //scripts to be dragged in the editor
    public GameManager gameManager;

    public MenuScreen menuScreen;
    public SettingsScreen settingsScreen;
    public EndScreen endScreen;
    public HUDScreen hudScreen;
    public PauseScreen pauseScreen;

    AudioSource[] menuMusics;

    // Start is called before the first frame update
    void Start()
    {
        //grabs the audio source components and stores in an array
        menuMusics = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //function to disable the menu, communicates with the game manager and opens the HUD
    public void StartGame()
    {
        gameManager.GameBegin();
        if (menuMusics[0].isPlaying)
        {
            menuMusics[0].Stop();
        }
        if (menuMusics[1].isPlaying == false)
        {
            menuMusics[1].Play();
        }
        hudScreen.gameObject.SetActive(true);
    }

    //function to disable every other screen and open the menu
    public void OpenMenu()
    {
        hudScreen.gameObject.SetActive(false);
        endScreen.gameObject.SetActive(false);
        menuScreen.gameObject.SetActive(true);
        if (menuMusics[0].isPlaying == false)
        {
            menuMusics[0].Play();
        }
        if (menuMusics[1].isPlaying)
        {
            menuMusics[1].Stop();
        }
    }

    //function to disable every other screen and open the settings menu
    public void OpenSettings()
    {
        settingsScreen.gameObject.SetActive(true);
        settingsScreen.Activate();
        menuScreen.gameObject.SetActive(false);
    }

    //function to disable every other screen except the HUD and open the ending screen
    public void OpenEndScreen()
    {
        endScreen.gameObject.SetActive(true);
        menuScreen.gameObject.SetActive(false);
        settingsScreen.gameObject.SetActive(false);
    }

    //function to open the pause menu
    public void PauseGame()
    {
        pauseScreen.gameObject.SetActive(true);
    }

    //function to close the pause menu
    public void ResumeGame()
    {
        pauseScreen.gameObject.SetActive(false);
    }

    //function that tells the game manager that the resume button has been pressed in the pause menu
    public void ResumeGameFromPause()
    {
        gameManager.GameResume();
    }

    //sets the level text in the HUD and moves it across the screen
    public void HUDNextLevelText(string text)
    {
        hudScreen.UpdateText(text);
        hudScreen.move = true;
    }

    //function to make one of the hearts visible in the HUD disappear
    public void HUDLoseLife()
    {
        hudScreen.LoseLife();
    }

    //function to reset the HUD
    public void HUDReset()
    {
        hudScreen.Reset();
    }
}
