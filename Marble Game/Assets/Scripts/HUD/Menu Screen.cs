using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour
{
    public HUDManager HUDManager;

    bool firstStartup = true;
    public float backdropMoveSpeed = 0.04f;

    Button startButton;
    Button settingsButton;

    AudioSource tickSound;

    List<BackdropMove> backdropMarbles = new List<BackdropMove>();

    // Start is called before the first frame update
    void Start()
    {
        //grabs all of the necessary components and adds listeners
        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child.name == "Start Button")
            {
                startButton = child.GetComponent<Button>();
            }
            if (child.name == "Settings Button")
            {
                settingsButton = child.GetComponent<Button>();
            }
            if (child.GetComponent<BackdropMove>() != null)
            {
                backdropMarbles.Add(child.GetComponent<BackdropMove>());
            }
        }
        startButton.onClick.AddListener(startButtonClicked);
        settingsButton.onClick.AddListener(SettingsButtonClicked);
        tickSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //upon first startup, spawn the balls the move across the menu
        if (firstStartup)
        {
            for (int x = 0; x < backdropMarbles.Count; x++)
            {
                backdropMarbles[x].Activate(500, backdropMoveSpeed);
            }
            firstStartup = false;
        }
    }

    //listener to start the game
    void startButtonClicked()
    {
        HUDManager.StartGame();
        tickSound.Play();
        gameObject.SetActive(false);
    }

    //listener to open the settings menu
    void SettingsButtonClicked()
    {
        HUDManager.OpenSettings();
        tickSound.Play();
    }
}
