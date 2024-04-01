using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    public HUDManager HUDManager;

    Button restartButton;
    Button menuButton;
    Button resumeButton;

    AudioSource tickSound;

    // Start is called before the first frame update
    void Start()
    {
        //grabs each button and gives it a listener
        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child.name == "Restart Button")
            {
                restartButton = child.GetComponent<Button>();
            }
            if (child.name == "Main Menu Button")
            {
                menuButton = child.GetComponent<Button>();
            }
            if (child.name == "Resume Button")
            {
                resumeButton = child.GetComponent<Button>();
            }
        }
        restartButton.onClick.AddListener(Restart);
        menuButton.onClick.AddListener(OpenMenu);
        resumeButton.onClick.AddListener(Resume);
        tickSound = GetComponent<AudioSource>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //listener that calls HUDmanager to restart the game
    void Restart()
    {
        HUDManager.StartGame();
        tickSound.Play();
        gameObject.SetActive(false);
    }

    //listener that calls HUDmanager to open the menu
    void OpenMenu()
    {
        HUDManager.OpenMenu();
        tickSound.Play();
        gameObject.SetActive(false);
    }

    //listener that calls HUDmanager to resume the game
    void Resume()
    {
        HUDManager.ResumeGameFromPause();
        tickSound.Play();
    }
}
