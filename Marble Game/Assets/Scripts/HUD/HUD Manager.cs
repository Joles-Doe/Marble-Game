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
        hudScreen.Activate();
    }

    public void OpenMenu()
    {
        hudScreen.Deactivate();
        endScreen.Deactivate();
        menuScreen.Activate();
    }

    public void OpenInstructions()
    {
        instructionsScreen.Activate();
        menuScreen.Deactivate();
    }

    public void OpenEndScreen()
    {
        endScreen.Activate();
        menuScreen.Deactivate();
        instructionsScreen.gameObject.SetActive(false);
    }

    public void HUDNextLevelText(string text)
    {
        hudScreen.UpdateText(text);
        hudScreen.move = true;
    }
}
