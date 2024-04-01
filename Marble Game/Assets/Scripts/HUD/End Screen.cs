using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public GameManager gameManager;
    public HUDManager HUDManager;

    Button restartButton;
    Button menuButton;

    AudioSource tickSound;

    // Start is called before the first frame update
    void Start()
    {
        //find each specific child and sets listeners for them before setting the object to false
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
        }
        restartButton.onClick.AddListener(Restart);
        menuButton.onClick.AddListener(OpenMenu);
        tickSound = GetComponent<AudioSource>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //button listener to restart the game
    void Restart()
    {
        HUDManager.StartGame();
        tickSound.Play();
        gameObject.SetActive(false);
    }

    //button listener to open the main menu
    void OpenMenu()
    {
        HUDManager.OpenMenu();
        tickSound.Play();
        gameObject.SetActive(false);
    }
}
