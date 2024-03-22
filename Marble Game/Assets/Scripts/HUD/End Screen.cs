using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public GameManager gameManager;
    public HUDManager HUDManager;

    Canvas canvas;

    Button restartButton;
    Button menuButton;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();

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
        Deactivate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        canvas.enabled = true;
    }

    public void Deactivate()
    {
        canvas.enabled = false;
        Debug.Log(canvas.enabled);
    }

    void Restart()
    {
        gameManager.GameBegin();
        Deactivate();
    }

    void OpenMenu()
    {
        //HUDManager.OpenMenu();
        Deactivate();
    }
}
