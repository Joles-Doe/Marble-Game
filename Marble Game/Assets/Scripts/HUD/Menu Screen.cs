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
    int backdropTimer;

    Button startButton;
    Button instructionsButton;

    AudioSource tickSound;

    List<BackdropMove> backdropMarbles = new List<BackdropMove>();

    // Start is called before the first frame update
    void Start()
    {

        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child.name == "Start Button")
            {
                startButton = child.GetComponent<Button>();
            }
            if (child.name == "Instructions Button")
            {
                instructionsButton = child.GetComponent<Button>();
            }
            if (child.GetComponent<BackdropMove>() != null)
            {
                backdropMarbles.Add(child.GetComponent<BackdropMove>());
            }
        }
        startButton.onClick.AddListener(startButtonClicked);
        instructionsButton.onClick.AddListener(instructionsButtonClicked);
        tickSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (firstStartup)
        {
            for (int x = 0; x < backdropMarbles.Count; x++)
            {
                backdropMarbles[x].Activate(x * 500, backdropMoveSpeed);
            }
            firstStartup = false;
        }
    }

    void startButtonClicked()
    {
        HUDManager.StartGame();
        tickSound.Play();
        gameObject.SetActive(false);
    }

    void instructionsButtonClicked()
    {
        HUDManager.OpenInstructions();
        tickSound.Play();
    }
}
