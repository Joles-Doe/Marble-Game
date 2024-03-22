using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour
{
    public HUDManager HUDManager;

    //List<GameObject> screenChildren = new List<GameObject>();

    Canvas canvas;

    Button startButton;
    Button instructionsButton;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();

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
        }
        startButton.onClick.AddListener(startButtonClicked);
        instructionsButton.onClick.AddListener(instructionsButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startButtonClicked()
    {
        HUDManager.StartGame();
        Deactivate();
    }

    void instructionsButtonClicked()
    {
        HUDManager.OpenInstructions();
        Deactivate();
    }

    public void Activate()
    {
        canvas.enabled = true;
    }

    public void Deactivate()
    {
        canvas.enabled = false;
    }
}
