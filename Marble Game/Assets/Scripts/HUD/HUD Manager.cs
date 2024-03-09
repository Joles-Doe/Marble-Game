using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public GameManager gameManager;

    public MenuScreen menuScreen;
    public InstructionsScreen instructionsScreen;
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
        hudScreen.gameObject.SetActive(true);
    }

    public void OpenInstructions()
    {
        instructionsScreen.Activate();
    }

    public void HUDNextLevelText(string text)
    {
        hudScreen.UpdateText(text);
        hudScreen.move = true;
    }
}
