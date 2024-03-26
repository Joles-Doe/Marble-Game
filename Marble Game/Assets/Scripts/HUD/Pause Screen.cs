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

    // Start is called before the first frame update
    void Start()
    {
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
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Restart()
    {
        HUDManager.StartGame();
        gameObject.SetActive(false);
    }

    void OpenMenu()
    {
        HUDManager.OpenMenu();
        gameObject.SetActive(false);
    }

    void Resume()
    {
        HUDManager.ResumeGameFromPause();
    }
}
