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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        gameManager.GameBegin();
        hudScreen.gameObject.SetActive(true);
    }

    public void OpenMenu()
    {
        hudScreen.gameObject.SetActive(false);
        endScreen.gameObject.SetActive(false);
        menuScreen.gameObject.SetActive(true);
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
}
