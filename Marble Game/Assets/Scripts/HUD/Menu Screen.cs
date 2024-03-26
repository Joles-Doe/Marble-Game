using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour
{
    public HUDManager HUDManager;

    Button startButton;
    Button instructionsButton;

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
        gameObject.SetActive(false);
    }

    void instructionsButtonClicked()
    {
        HUDManager.OpenInstructions();
    }
}
