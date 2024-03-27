using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public GameManager gameManager;

    public MenuScreen menuScreen;
    public InstructionsScreen instructionsScreen;
    public EndScreen endScreen;
    public HUDScreen hudScreen;
    public PauseScreen pauseScreen;

    AudioSource[] menuMusics;

    // Start is called before the first frame update
    void Start()
    {
        menuMusics = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        gameManager.GameBegin();
        menuMusics[0].Stop();
        menuMusics[1].Play();
        hudScreen.gameObject.SetActive(true);
    }

    public void OpenMenu()
    {
        hudScreen.gameObject.SetActive(false);
        endScreen.gameObject.SetActive(false);
        menuScreen.gameObject.SetActive(true);
        menuMusics[0].Play();
        menuMusics[1].Stop();
    }

    public void OpenInstructions()
    {
        instructionsScreen.gameObject.SetActive(true);
        menuScreen.gameObject.SetActive(false);
    }

    public void OpenEndScreen()
    {
        endScreen.gameObject.SetActive(true);
        menuScreen.gameObject.SetActive(false);
        instructionsScreen.gameObject.SetActive(false);
    }

    public void PauseGame()
    {
        pauseScreen.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        pauseScreen.gameObject.SetActive(false);
    }

    public void ResumeGameFromPause()
    {
        gameManager.GameResume();
    }

    public void HUDNextLevelText(string text)
    {
        hudScreen.UpdateText(text);
        hudScreen.move = true;
    }

    public void HUDLoseLife()
    {
        hudScreen.LoseLife();
    }

    public void HUDReset()
    {
        hudScreen.Reset();
    }
}
